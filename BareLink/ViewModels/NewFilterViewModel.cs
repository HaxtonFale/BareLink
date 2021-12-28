using BareLink.Models;
using Xamarin.Forms;

namespace BareLink.ViewModels
{
    public class NewFilterViewModel : BaseViewModel
    {
        private string _name;
        private string _description;
        private string _pattern;

        public NewFilterViewModel()
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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var newFilter = new Filter
            {
                Name = Name,
                Description = Description,
                Pattern = Pattern,
                Active = true
            };

            await FiltersService.SaveFilterAsync(newFilter);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
