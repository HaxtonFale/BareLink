using BareLink.ViewModels;
using Xamarin.Forms.Xaml;

namespace BareLink.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = new SettingsViewModel();
        }
    }
}