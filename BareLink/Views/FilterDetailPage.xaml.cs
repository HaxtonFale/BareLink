using BareLink.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BareLink.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterDetailPage : ContentPage
    {
        public FilterDetailPage()
        {
            InitializeComponent();
            BindingContext = new FilterDetailViewModel();
        }
    }
}