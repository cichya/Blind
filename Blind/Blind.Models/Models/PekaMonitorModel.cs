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
	public class Bollard
	{
		public string symbol { get; set; }
		public string tag { get; set; }
		public string name { get; set; }
		public bool mainBollard { get; set; }
	}

	public class Time
	{
		public bool realTime { get; set; }
		public int minutes { get; set; }
		public string direction { get; set; }
		public bool onStopPoint { get; set; }
		public string departure { get; set; }
		public string line { get; set; }
	}

	public class Success
	{
		public Bollard bollard { get; set; }
		public List<Time> times { get; set; }
	}

	public class PekaMonitorModel
	{
		public Success success { get; set; }
	}
}