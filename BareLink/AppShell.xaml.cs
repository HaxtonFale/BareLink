using BareLink.Views;
using Xamarin.Forms;

namespace BareLink
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(FilterDetailPage), typeof(FilterDetailPage));
            Routing.RegisterRoute(nameof(NewFilterPage), typeof(NewFilterPage));
        }

    }
}
