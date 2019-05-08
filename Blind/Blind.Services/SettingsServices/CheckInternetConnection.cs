using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform;
using Android.App.Usage;
using MvvmCross.Plugins.Network.Reachability;

namespace Blind.Services.SettingsServices
{
	public class CheckInternetConnection : ISettingsService
	{
		private IMvxReachability _reachability;

		public CheckInternetConnection()
		{
			this._reachability = Mvx.Resolve<IMvxReachability>();
		}

		public bool Check()
		{
			return _reachability.IsHostReachable("www.google.com");
		}
	}
}