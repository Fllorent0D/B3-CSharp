
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

namespace SmartApp
{
	[Activity(Label = "Search")]
	public class Search : Activity
	{
		public Toolbar mTool;
		public TextView mText;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.search);
			mTool = FindViewById<Toolbar>(Resource.Id.toolbarSearch);
			SetActionBar(mTool);
			ActionBar.Title = "Rechercher";
			mText = FindViewById<TextView>(Resource.Id.editText1);

			mText.KeyPress += (object sender, View.KeyEventArgs e) =>
			{
				e.Handled = false;
				if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
				{
					Toast.MakeText(this, mText.Text, ToastLength.Short).Show();
					e.Handled = true;
				}
			};
		}
	}
}
