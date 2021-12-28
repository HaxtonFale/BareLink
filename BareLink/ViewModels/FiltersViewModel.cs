﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using BareLink.Models;
using BareLink.Views;
using Xamarin.Forms;

namespace BareLink.ViewModels
{
    public class FiltersViewModel : BaseViewModel
    {
        private Filter _selectedFilter;

        public ObservableCollection<Filter> Filters { get; }
        public Command LoadFiltersCommand { get; }
        public Command AddFilterCommand { get; }
        public Command<Filter> FilterTapped { get; }

        public FiltersViewModel()
        {
            Title = "Browse";
            Filters = new ObservableCollection<Filter>();
            LoadFiltersCommand = new Command(async () => await ExecuteLoadItemsCommand());

            FilterTapped = new Command<Filter>(OnItemSelected);

            AddFilterCommand = new Command(OnAddItem);
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Filters.Clear();
                var items = await FiltersService.GetFiltersAsync();
                foreach (var item in items)
                {
                    Filters.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Filter SelectedItem
        {
            get => _selectedFilter;
            set
            {
                SetProperty(ref _selectedFilter, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewFilterPage));
        }

        async void OnItemSelected(Filter item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(FilterDetailPage)}?{nameof(FilterDetailViewModel.FilterId)}={item.Id}");
        }
    }
}