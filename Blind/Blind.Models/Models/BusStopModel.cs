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

namespace Blind.Models.Models
{
	public class Geometry
	{
		public List<double> coordinates { get; set; }
		public string type { get; set; }
	}

	public class Properties
	{
		public string zone { get; set; }
		public string route_type { get; set; }
		public string headsigns { get; set; }
		public string stop_name { get; set; }
	}

	public class Feature
	{
		public Geometry geometry { get; set; }
		public string id { get; set; }
		public string type { get; set; }
		public Properties properties { get; set; }
	}

	public class Properties2
	{
		public string code { get; set; }
	}

	public class Crs
	{
		public string type { get; set; }
		public Properties2 properties { get; set; }
	}

	public class BusStopModel
	{
		public List<Feature> features { get; set; }
		public Crs crs { get; set; }
		public string type { get; set; }
	}

}