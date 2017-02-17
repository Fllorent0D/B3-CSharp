using Android.App;
using Android.Widget;
using Android.OS;
using DTOLibrary;
using System.Collections.Generic;
using System.Net;
using System;
using System.Text;
using Newtonsoft.Json;

namespace SmartApp
{
    [Activity(Label = "Vidéothèque", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public ListView mView;
        public List<FilmDTO> items;
        public WebClient mWeb;
        public Uri murl = new Uri("http://192.168.1.7/api/films");
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            mView = FindViewById<ListView>(Resource.Id.FilmListView);

            items = new List<FilmDTO>();
            items.Add(new FilmDTO { titre = "In hac habitasse platea dictumst. Maecenas fringilla eros id leo commodo." });
            mWeb = new WebClient();
            mWeb.DownloadDataAsync(murl);
            mWeb.DownloadDataCompleted += MWeb_DownloadDataCompleted;
            
            //ArrayAdapter < string > adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);
            /*FilmViewAdapter adapter = new FilmViewAdapter(this, items);
            mView.Adapter = adapter;*/
        }

        private void MWeb_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                string json = Encoding.UTF8.GetString(e.Result);
                items = JsonConvert.DeserializeObject<List<FilmDTO>>(json);
                FilmViewAdapter adapter = new FilmViewAdapter(this, items);
                mView.Adapter = adapter;
            });
            
        }
    }
}

