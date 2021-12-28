using BareLink.Models;
using BareLink.ViewModels;
using Xamarin.Forms;

namespace BareLink.Views
{
    public partial class NewFilterPage : ContentPage
    {
        public Filter Filter { get; set; }

        public NewFilterPage()
        {
            InitializeComponent();
            BindingContext = new NewFilterViewModel();
        }
    }
}