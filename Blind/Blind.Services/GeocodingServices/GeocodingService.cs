using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform.Droid.Platform;

namespace Blind.Services.GeocodingServices
{
	public class GeocodingService:IGeocodingService
	{
		private IMvxAndroidCurrentTopActivity _currentActivity;

		public GeocodingService(IMvxAndroidCurrentTopActivity activity)
		{
			this._currentActivity = activity;
		}

		public async Task<string> Geocoding(double latitude, double longitude)
		{
			var geo = new Geocoder(_currentActivity.Activity);

			var address = await geo.GetFromLocationAsync(latitude, longitude, 1);

			if (address.Any())
			{
				return String.Format("{0}, {1}, {2}", address[0].Thoroughfare, address[0].SubThoroughfare, address[0].Locality);
			}
			else
			{
				return string.Empty;
			}
		}

		public async Task<Dictionary<string, double>> ReverseGeocoding(string address)
		{
			var geo = new Geocoder(_currentActivity.Activity);

			var result = await geo.GetFromLocationNameAsync(address, 1);

			return new Dictionary<string, double>
			{
				{"Longitude", result[0].Longitude },
				{"Latitude", result[0].Latitude },
			};
		}
	}
}