using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Blind.Services.SerializationService;
using MvvmCross.Platform;
using MvvmCross.Plugins.PictureChooser;
using MvvmCross.Plugins.PictureChooser.Droid;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin = MvvmCross.Plugins.PictureChooser.Droid.Plugin;

namespace Blind.Services.PhotoService
{
	public class PhotoService : IPhotoService
	{
		private const int MaxPixelDimension = 5280;
		private const int DefaultJpegQuality = 90;

		private ISerializationService _serializationService;

		public byte[] Photo
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public event EventHandler PhotoChanged;

		public PhotoService(ISerializationService serializationService)
		{
			this._serializationService = serializationService;
		}

		public async Task GetPhoto()
		{
			try
			{
				if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakeVideoSupported)
				{
					var cameraMediaOptions = new StoreCameraMediaOptions()
					{
						DefaultCamera = CameraDevice.Rear,
						CompressionQuality = 92,
						PhotoSize = PhotoSize.Medium,
					};


					var file = await CrossMedia.Current.TakePhotoAsync(cameraMediaOptions);
					
					//return _serializationService.MediaFileToStream(file);
				}
				else
				{
					throw new Exception("Camera not Available");
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}