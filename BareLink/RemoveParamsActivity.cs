using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Google.Android.Material.Snackbar;
using Xamarin.Essentials;

namespace BareLink
{
    [Activity(Label = "RemoveParamsActivity", Theme = "@android:style/Theme.NoDisplay")]
    [IntentFilter(new []{Intent.ActionSend}, Categories = new[] { Intent.CategoryDefault }, DataMimeType = "text/simple", Label = "Remove GET Params")]
    public class RemoveParamsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            if (!(Intent is {Action: Intent.ActionSend})) return;
            var text = Intent.Extras?.GetString(Intent.ExtraText);
            if (text == null) return;
            var filters = Database.GetActiveFilters();
            foreach (var filter in filters)
            {
                if (!filter.TryMatch(text, out var filteredResult)) continue;
                Clipboard.SetTextAsync(filteredResult).Wait();
                ShowSnackbar("Filtered URL copied to clipboard.");
                return;
            }
            Clipboard.SetTextAsync(text).Wait();
            ShowSnackbar("No applicable filter found. Copied content as-is.");
            Finish();
        }

        private void ShowSnackbar(string text)
        {
            var view = Window?.DecorView?.RootView;
            Snackbar.Make(view, text, BaseTransientBottomBar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
        }
    }
}