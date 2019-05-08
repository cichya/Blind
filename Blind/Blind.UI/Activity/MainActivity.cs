using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech;
using Android.Util;
using Android.Views;
using Android.Widget;
using Blind.ViewModels.ViewModels;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Plugins.PictureChooser;

namespace Blind.UI.Activity
{
	[Activity(Label = "Blind", MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar")]
	public class MainActivity:MvxActivity
	{
		public new MainViewModel ViewModel
		{
			get { return (MainViewModel) base.ViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnViewModelSet()
		{
			base.OnViewModelSet();
			SetContentView(Resource.Layout.MainView);
		}
	}
}