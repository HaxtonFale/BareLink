using BareLink.Models;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BareLink.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace BareLink.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public const string AppThemePropertyName = "appTheme";
        public Command ReseedDatabaseCommand { get; }
        public Command ExportFiltersCommand { get; }

        private readonly SettingsPage _settingsPage;
        private ThemeItem _selectedTheme;

        public SettingsViewModel(SettingsPage settingsPage)
        {
            _settingsPage = settingsPage;
            ReseedDatabaseCommand = new Command(ExecuteReseedDatabaseCommand);

            if (Preferences.ContainsKey(AppThemePropertyName))
            {
                SelectedTheme = Themes.Find(t => t.Theme == (OSAppTheme)Preferences.Get(AppThemePropertyName, (int)OSAppTheme.Unspecified));
            }
            else
            {
                SelectedTheme = Themes.Find(t => t.Theme == Application.Current.UserAppTheme);
            }
        }

        public List<ThemeItem> Themes => new List<ThemeItem>
            {new ThemeItem(OSAppTheme.Unspecified), new ThemeItem(OSAppTheme.Dark), new ThemeItem(OSAppTheme.Light)};

        public ThemeItem SelectedTheme
        {
            get => _selectedTheme;
            set => SetProperty(ref _selectedTheme, value, onChanged: UpdateAppTheme);
        }

        private void UpdateAppTheme()
        {
            Application.Current.UserAppTheme = SelectedTheme.Theme;
            Preferences.Set(AppThemePropertyName, (int)SelectedTheme.Theme);
        }

        private async void ExecuteReseedDatabaseCommand()
        {
            var response =
                await _settingsPage.DisplayAlert("Are you sure?", "This process will reset ALL your filters to default, and CANNOT be reversed!", "Yes", "No");
            if (!response) return;
            await PopupNavigation.Instance.PushAsync(new BusyPopupPage());
            await FiltersService.DeleteAllFiltersAsync();
            using var resource = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("BareLink.Models.filters.default.json");
            if (resource == null) return;
            using var reader = new StreamReader(resource);
            var filters = await reader.ReadToEndAsync();
            await FiltersService.ImportJsonAsync(filters);
            await PopupNavigation.Instance.PopAsync();
        }
    }
}