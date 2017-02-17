using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DTOLibrary;
using Newtonsoft.Json;
namespace SmartApp
{
    [Activity(Label = "Film")]
    public class Film : Activity
    {
		private Toolbar mToolbar;
		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.film);
			mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar2);
			SetActionBar(mToolbar);
			ActionBar.Title = "Détails";  
			FilmDTO f = JsonConvert.DeserializeObject<FilmDTO>(Intent.GetStringExtra("film"));
			TextView t = FindViewById<TextView>(Resource.Id.txtTitle);
			TextView r = FindViewById<TextView>(Resource.Id.txtRuntime);
			t.Text = f.titre;
			r.Text = f.runtime.ToString() ;
            ImageView img = FindViewById<ImageView>(Resource.Id.imgPoster);
            Koush.UrlImageViewHelper.SetUrlDrawable(img, "http://image.tmdb.org/t/p/w185/"+f.poster_path, null, 60000);
			ListView acLis = FindViewById<ListView>(Resource.Id.ActorListView);
			ListView reaLis = FindViewById<ListView>(Resource.Id.RealisatorListView);
			ListView genreLis = FindViewById<ListView>(Resource.Id.GenresListView);

			List<string> ac = new List<string>();
			foreach (var item in f.actors)
				ac.Add(item.name + " ("+ item.character + ")");

			List<string> rea = new List<string>();
			foreach (var item in f.realisateurs)
				rea.Add(item.Name);

			List<string> genre = new List<string>();
			foreach (var item in f.genres)
				genre.Add(item.Name);

			ArrayAdapter<String> acAdap = new ArrayAdapter<String>(this,Android.Resource.Layout.SimpleListItem1, ac);
			ArrayAdapter<String> reaAdap = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, rea);
			ArrayAdapter<String> genreAdap = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, genre);

			acLis.Adapter = acAdap;
			reaLis.Adapter = reaAdap;
			genreLis.Adapter = genreAdap;
			

			//Android.Widget.Toast.MakeText(this, f.ToString(), Android.Widget.ToastLength.Short).Show();

			// Create your application here
		}
    }
}