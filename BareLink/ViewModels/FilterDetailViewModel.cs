using System;
using System.Diagnostics;
using BareLink.Models;
using BareLink.Views;
using Xamarin.Forms;

namespace BareLink.ViewModels
{
    [QueryProperty(nameof(FilterId), nameof(FilterId))]
    public class FilterDetailViewModel : BaseViewModel
    {
        private readonly FilterDetailPage _filterDetailPage;
        private int _filterId;
        private string _name;
        private string _description;
        private string _pattern;
        private bool _active;

        public Command EditFilterCommand { get; }
        public Command DeleteFilterCommand { get; }

        public FilterDetailViewModel(FilterDetailPage filterDetailPage)
        {
            _filterDetailPage = filterDetailPage;
            EditFilterCommand = new Command(ExecuteEditFilterCommand);
            DeleteFilterCommand = new Command(ExecuteDeleteFilterCommand);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string Pattern
        {
            get => _pattern;
            set => SetProperty(ref _pattern, value);
        }

        public bool Active
        {
            get => _active;
            set => SetProperty(ref _active, value, onChanged: UpdateFilter);
        }

        public int FilterId
        {
            get => _filterId;
            set
            {
                _filterId = value;
                LoadFilterId(value);
            }
        }
        public async void LoadFilterId(int filterId)
        {
            try
            {
                var filter = await FiltersService.GetFilterAsync(filterId);
                Name = filter.Name;
                Description = filter.Description;
                Pattern = filter.Pattern;
                Active = filter.Active;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Filter");
            }
        }

        private Filter Filter => new Filter
        {
            Id = FilterId,
            Name = Name,
            Description = Description,
            Pattern = Pattern,
            Active = Active
        };

        private async void UpdateFilter()
        {
            try
            {
                await FiltersService.SaveFilterAsync(Filter);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Save Filter");
            }
        }

        private async void ExecuteEditFilterCommand()
        {
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EditFilterPage)}?{nameof(EditFilterViewModel.FilterId)}={_filterId}");
        }

        private async void ExecuteDeleteFilterCommand()
        {
            var response =
                await _filterDetailPage.DisplayAlert("Are you sure?", "This process cannot be reversed", "Yes", "No");
            if (!response) return;
            await FiltersService.DeleteFilterAsync(Filter);
            await Shell.Current.GoToAsync("..");
        }
    }
}