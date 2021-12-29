using BareLink.Services;
using Xamarin.Forms;

namespace BareLink
{
    public partial class App
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<DatabaseFiltersService>();
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
