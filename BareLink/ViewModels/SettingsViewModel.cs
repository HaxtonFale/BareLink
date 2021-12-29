using BareLink.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace BareLink.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private ThemeItem _selectedTheme;
        public const string AppThemePropertyName = "appTheme";

        public SettingsViewModel()
        {
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
    }
}