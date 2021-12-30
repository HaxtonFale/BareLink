using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using BareLink.Models;
using BareLink.Views;
using Xamarin.Forms;

namespace BareLink.ViewModels
{
    public class FiltersViewModel : BaseViewModel
    {
        private FilterViewModel _selectedFilter;

        public ObservableCollection<FilterViewModel> Filters { get; }
        public Command LoadFiltersCommand { get; }
        public Command AddFilterCommand { get; }
        public Command<FilterViewModel> FilterTapped { get; }
        public Command<FilterViewModel> FilterToggled { get; }

        public FiltersViewModel()
        {
            Title = "Filters";
            Filters = new ObservableCollection<FilterViewModel>();
            LoadFiltersCommand = new Command(ExecuteLoadItemsCommand);

            FilterTapped = new Command<FilterViewModel>(OnFilterSelected);
            FilterToggled = new Command<FilterViewModel>(OnFilterToggled);

            AddFilterCommand = new Command(OnAddFilter);
        }

        private async void ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Filters.Clear();
                var items = await FiltersService.GetFiltersAsync();
                foreach (var item in items)
                {
                    Filters.Add(new FilterViewModel(item));
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

        public FilterViewModel SelectedItem
        {
            get => _selectedFilter;
            set
            {
                SetProperty(ref _selectedFilter, value);
                OnFilterSelected(value);
            }
        }

        private async void OnAddFilter(object obj)
        {
            await Shell.Current.GoToAsync(nameof(EditFilterPage));
        }

        private async void OnFilterSelected(FilterViewModel filter)
        {
            if (filter == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(FilterDetailPage)}?{nameof(FilterDetailViewModel.FilterId)}={filter.Id}");
        }

        private async void OnFilterToggled(FilterViewModel filter)
        {
            if (filter == null)
                return;

            filter.Toggle();
            await filter.SaveAsync();
        }
    }
}