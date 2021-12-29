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
        private Filter _selectedFilter;

        public ObservableCollection<Filter> Filters { get; }
        public Command LoadFiltersCommand { get; }
        public Command AddFilterCommand { get; }
        public Command<Filter> FilterTapped { get; }

        public FiltersViewModel()
        {
            Title = "Browse";
            Filters = new ObservableCollection<Filter>();
            LoadFiltersCommand = new Command(ExecuteLoadItemsCommand);

            FilterTapped = new Command<Filter>(OnItemSelected);

            AddFilterCommand = new Command(OnAddItem);
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
            await Shell.Current.GoToAsync(nameof(EditFilterPage));
        }

        private async void OnItemSelected(Filter filter)
        {
            if (filter == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(FilterDetailPage)}?{nameof(FilterDetailViewModel.FilterId)}={filter.Id}");
        }
    }
}