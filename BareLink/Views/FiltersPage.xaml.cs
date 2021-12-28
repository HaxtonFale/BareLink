using BareLink.ViewModels;
using Xamarin.Forms;

namespace BareLink.Views
{
    public partial class FiltersPage : ContentPage
    {
        FiltersViewModel _viewModel;

        public FiltersPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new FiltersViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}