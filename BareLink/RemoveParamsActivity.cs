using Android.App;
using Android.Content;
using Android.OS;
using System.IO;
using Environment = System.Environment;

namespace BareLink
{
    [Activity(Label = "RemoveParamsActivity")]
    [IntentFilter(new []{Intent.ActionSend}, Categories = new[] { Intent.CategoryDefault }, DataMimeType = "text/*", Label = "Remove GET Params")]
    public class RemoveParamsActivity : Activity
    {
        private static Database _database;

        public static Database Database
        {
            get
            {
                return _database ??= new Database(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "filters.db3"));
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            if (Intent?.Action == Intent.ActionSend)
            {

            }

            base.OnCreate(savedInstanceState);
        }
    }
}