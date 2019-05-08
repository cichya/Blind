using System;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Views;
using Blind.Services.PhotoService;
using MvvmCross.Platform.Droid.Platform;
using Camera = Android.Hardware.Camera;

namespace Blind.Services.PhotoServices
{
	public class PhotoService2: Java.Lang.Object, IPhotoService, Camera.IPictureCallback, Camera.IPreviewCallback, Camera.IShutterCallback, ISurfaceHolderCallback
	{
		private IMvxAndroidCurrentTopActivity _activity;

		Camera _camera;

		private byte[] _photo;

		public byte[] Photo
		{
			get { return _photo; }
			set
			{
				_photo = value;
				PhotoChanged?.Invoke(this, null);
			}
		}

		public event EventHandler PhotoChanged;

		public PhotoService2(IMvxAndroidCurrentTopActivity activity)
		{
			this._activity = activity;
		}

		public async Task GetPhoto()
		{
			_camera = Camera.Open();
			var parameters = _camera.GetParameters();

			var sizes = parameters.SupportedPictureSizes;

			int index = 0;

			for (int i = 0; i < sizes.Count; i++)
			{
				if (sizes[i].Width > 1200 && sizes[i].Width < 1300)
				{
					index = i;
				}
			}

			parameters.SetPictureSize(sizes[index].Width, sizes[index].Height);
			parameters.SetRotation(90);
			parameters.SceneMode = Camera.Parameters.SceneModeAuto;
			parameters.WhiteBalance = Camera.Parameters.WhiteBalanceAuto;
			parameters.FocusMode = Camera.Parameters.FocusModeContinuousPicture;
			parameters.PictureFormat = ImageFormatType.Jpeg;
			parameters.JpegQuality = 100;
			_camera.SetParameters(parameters);
			_camera.SetPreviewCallback(this);
			_camera.Lock();
			SurfaceTexture st = new SurfaceTexture(100);
			_camera.SetPreviewTexture(st);
			_camera.StartPreview();
			
			await TakePicture();
		}

		private async Task TakePicture()
		{

			await Task.Factory.StartNew(() =>
			{
				System.Threading.Thread.Sleep(1000);
				_camera.TakePicture(this, this, this);
			});
		}

		public void OnPictureTaken(byte[] data, Camera camera)
		{
			if (data != null)
			{
				Photo = data;
				_camera.Unlock();
				_camera.StopPreview();
				_camera.Release();
			}
		}

		public void OnPreviewFrame(byte[] data, Camera camera)
		{
			//throw new NotImplementedException();
		}

		public void OnShutter()
		{
			//throw new NotImplementedException();
		}

		public void SurfaceChanged(ISurfaceHolder holder, Format format, int width, int height)
		{
			//throw new NotImplementedException();
		}

		public void SurfaceCreated(ISurfaceHolder holder)
		{
			try
			{
				//var p = _camera.GetParameters();
				//p.PictureFormat = ImageFormatType.Jpeg;
				//_camera.SetParameters(p);
				//_camera.SetPreviewCallback(this);
				//_camera.Lock();
				//_camera.SetPreviewDisplay(holder);
				//_camera.StartPreview();
			}
			catch (Exception e)
			{
			}
		}

		public void SurfaceDestroyed(ISurfaceHolder holder)
		{
			//_camera.Unlock();
			//_camera.StopPreview();
			//_camera.Release();
		}
	}
}