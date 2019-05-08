using System.IO;
using Android.Graphics;
using Blind.Services.SerializationService;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;

namespace Blind.Services.SerializationServices
{
	public class SerializationService:ISerializationService
	{
		public byte[] BitmapToStream(Bitmap bitmap)
		{
			MemoryStream stream = new MemoryStream();
			bitmap.Compress(Bitmap.CompressFormat.Jpeg, 0, stream);
			byte[] bitmapData = stream.ToArray();
			return bitmapData;
		}

		public byte[] MediaFileToStream(MediaFile mediaFile)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				mediaFile.GetStream().CopyTo(memoryStream);
				mediaFile.Dispose();
				return memoryStream.ToArray();
			}
		}

		public object JsonDeserialize<T>(string obj)
		{
			return JsonConvert.DeserializeObject<T>(obj);
		}
	}
}