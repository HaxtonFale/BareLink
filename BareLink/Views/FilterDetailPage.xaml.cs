using BareLink.ViewModels;
using Xamarin.Forms.Xaml;

namespace BareLink.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterDetailPage
    {
        public FilterDetailPage()
        {
            InitializeComponent();
            BindingContext = new FilterDetailViewModel(this);
        }


    }
}