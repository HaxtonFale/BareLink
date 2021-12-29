using BareLink.ViewModels;

namespace BareLink.Views
{
    public partial class EditFilterPage
    {
        public EditFilterPage()
        {
            InitializeComponent();
            BindingContext = new EditFilterViewModel();
        }
    }
}