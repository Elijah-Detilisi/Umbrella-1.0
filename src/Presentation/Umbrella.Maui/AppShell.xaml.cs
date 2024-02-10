using Umbrella.Maui.Email.EmailDetail.Pages;
using Umbrella.Maui.Email.EmailListing.Pages;

namespace Umbrella.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(EmailListingPage), typeof(EmailListingPage));
            Routing.RegisterRoute(nameof(EmailDetailPage), typeof(EmailDetailPage));
        }
    }
}
