using System;
using System.Diagnostics;
using BareLink.Models;
using Xamarin.Forms;

namespace BareLink.ViewModels
{
    [QueryProperty(nameof(FilterId), nameof(FilterId))]
    public class EditFilterViewModel : BaseViewModel
    {
        private string _name;
        private string _description;
        private string _pattern;
        private bool _active = true;
        private int _filterId;

        public EditFilterViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(_name)
                && !string.IsNullOrWhiteSpace(_pattern);
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
                FilterId = filter.Id;
                Name = filter.Name;
                Description = filter.Description;
                Pattern = filter.Pattern;
                _active = filter.Active;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Filter");
            }
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var filter = new Filter
            {
                Id = _filterId,
                Name = Name,
                Description = Description,
                Pattern = Pattern,
                Active = _active
            };

            await FiltersService.SaveFilterAsync(filter);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
