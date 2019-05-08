using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Media.Abstractions;

namespace Blind.Services.SerializationService
{
	public interface ISerializationService
	{
		byte[] BitmapToStream(Bitmap bitmap);
		byte[] MediaFileToStream(MediaFile mediaFile);
	}
}