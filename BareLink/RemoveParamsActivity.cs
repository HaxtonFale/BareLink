﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Xamarin.Essentials;

namespace BareLink
{
    [Activity(Label = "RemoveParamsActivity", Theme = "@android:style/Theme.NoDisplay")]
    [IntentFilter(new []{Intent.ActionSend}, Categories = new[] { Intent.CategoryDefault }, DataMimeType = "text/*", Label = "Remove GET Params")]
    public class RemoveParamsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (!(Intent is {Action: Intent.ActionSend})) return;
            var text = Intent.Extras?.GetString(Intent.ExtraText);
            if (text == null) return;
            var filters = Database.GetActiveFilters();
            foreach (var filter in filters)
            {
                if (!filter.TryMatch(text, out var filteredResult)) continue;
                Clipboard.SetTextAsync(filteredResult).Wait();
                Toast.MakeText(Application.Context, "Filtered URL copied to clipboard.", ToastLength.Long)?.Show();
                Finish();
                return;
            }
            Clipboard.SetTextAsync(text).Wait();
            Toast.MakeText(Application.Context, "No applicable filter found. Copied content as-is.", ToastLength.Long)?.Show();
            Finish();
        }
    }
}