using BareLink.Services;
using BareLink.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BareLink
{
    public partial class App
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<DatabaseFiltersService>();
            var theme = Preferences.Get(SettingsViewModel.AppThemePropertyName, (int)OSAppTheme.Unspecified);
            Current.UserAppTheme = (OSAppTheme)theme;
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
