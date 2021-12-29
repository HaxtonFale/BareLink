using BareLink.Views;
using Xamarin.Forms;

namespace BareLink
{
    public partial class AppShell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(FilterDetailPage), typeof(FilterDetailPage));
            Routing.RegisterRoute(nameof(EditFilterPage), typeof(EditFilterPage));
        }

    }
}
