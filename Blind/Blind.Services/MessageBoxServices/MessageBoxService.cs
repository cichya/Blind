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
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;

namespace Blind.Services.MessageBoxService
{
	public class MessageBoxService:IMessageBoxService
	{
		private IMvxAndroidCurrentTopActivity _currentActivity;

		public MessageBoxService(IMvxAndroidCurrentTopActivity activity)
		{
			this._currentActivity = activity;
		}

		public void ShowErrorMessageBox(string message)
		{
			var dialog = new AlertDialog.Builder(_currentActivity.Activity);
			dialog.SetMessage(message);
			dialog.SetNeutralButton("OK", (s, ev) =>
			{
				
			});
			dialog.Show();
		}
	}
}