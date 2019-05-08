using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Blind.Services.GeocodingServices
{
	public interface IGeocodingService
	{
		Task<string> Geocoding(double latitude, double longitude);
		Task<Dictionary<string, double>> ReverseGeocoding(string address);
	}
}