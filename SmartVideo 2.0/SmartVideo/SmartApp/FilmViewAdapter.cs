using System;
using Android.Widget;
using Android.Views;
using System.Collections.Generic;
using Android.Content;
using Android;
using DTOLibrary;


namespace SmartApp
{
	public class FilmViewAdapter : BaseAdapter<FilmDTO>
	{
		private List<FilmDTO> mItems;
		private Context mContext;

		public FilmViewAdapter(Context context, List<FilmDTO> items)
		{
			mItems = items;
			mContext = context;

		}


		public override FilmDTO this[int position]
		{
			get
			{
				return mItems[position];
			}
		}

		public override int Count
		{
			get
			{
				return mItems.Count;

			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;
			if (convertView == null)
			{
				row = LayoutInflater.From(mContext).Inflate(Resource.Layout.listview_row, null, false);
			}
			TextView txtV = row.FindViewById<TextView>(Resource.Id.txtTitle);
            TextView txtR = row.FindViewById<TextView>(Resource.Id.txtRuntime);
            txtR.Text = mItems[position].runtime + " minutes";
            txtV.Text = mItems[position].titre;
            ImageView img = row.FindViewById<ImageView>(Resource.Id.imgPoster);
            Koush.UrlImageViewHelper.SetUrlDrawable(img, "http://image.tmdb.org/t/p/w185/"+mItems[position].poster_path, null, 60000);
            return row;
		}
	}
}
