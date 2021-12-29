using BareLink.ViewModels;

namespace BareLink.Views
{
    public partial class FiltersPage
    {
        private readonly FiltersViewModel _viewModel;

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