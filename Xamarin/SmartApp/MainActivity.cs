using Android.App;
using Android.Widget;
using Android.OS;
using DTOLibrary;
using System.Collections.Generic;
using System.Net;
using System;
using System.Text;
using Newtonsoft.Json;
using Android.Content;

namespace SmartApp
{
    [Activity(Label = "Vidéothèque", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public ListView mView;
        public List<FilmDTO> items;
        public WebClient mWeb;
		public Toolbar mTool;
		public Button mSearch;

        public Uri murl = new Uri("http://10.59.22.57/api/films");
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
			mTool = FindViewById<Toolbar>(Resource.Id.toolbar);
			SetActionBar(mTool);
			ActionBar.Title = "Films disponibles";

			// Set our view from the "main" layout resource
			mView = FindViewById<ListView>(Resource.Id.FilmListView);
			mView.ItemClick += OnListItemClick;
			reloadFilm();

		}
		void reloadFilm()
		{
			mWeb = new WebClient();

			mWeb.DownloadDataAsync(murl);
			mWeb.DownloadDataCompleted += MWeb_DownloadDataCompleted;
			mWeb.Dispose();
		}
		private void MWeb_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			RunOnUiThread(() =>
			{
				try
				{
					string json = Encoding.UTF8.GetString(e.Result);
					items = JsonConvert.DeserializeObject<List<FilmDTO>>(json);
					FilmViewAdapter adapter = new FilmViewAdapter(this, items);
					mView.Adapter = adapter;
				}
				catch (Exception)
				{
					Android.Widget.Toast.MakeText(this, "Une erreur est survenue lors de la connexion avec le serveur central", Android.Widget.ToastLength.Short).Show();

				}
			});

		}
		void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var t = items[e.Position];
			Intent intent = new Intent(this, typeof(Film));
			intent.PutExtra("film", JsonConvert.SerializeObject(t));
			this.StartActivity(intent);
		}
		public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.top_menu, menu);
			return base.OnCreateOptionsMenu(menu);

		}
		public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
		{
			Android.Widget.Toast.MakeText(this, item.TitleFormatted, ToastLength.Short).Show();
			switch (item.TitleFormatted.ToString())
			{
				case "Recharger" :
					reloadFilm();
					break;
				case "Rechercher":
					Intent intent = new Intent(this, typeof(Search));
					this.StartActivity(intent);
					break;	
			}

			return base.OnOptionsItemSelected(item);
		}



    }
}

