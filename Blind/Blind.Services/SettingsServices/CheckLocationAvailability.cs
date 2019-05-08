using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using MvvmCross.Plugins.Location;

namespace Blind.Services.SettingsServices
{
	public class CheckLocationAvailability:ISettingsService
	{
		private IMvxAndroidCurrentTopActivity _activity;

		public CheckLocationAvailability()
		{
			this._activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
		}

		public bool Check()
		{
			LocationManager locMgr = _activity.Activity.GetSystemService(Context.LocationService) as LocationManager;

			string provider = LocationManager.GpsProvider;

			return locMgr.IsProviderEnabled(provider);
		}
	}
}