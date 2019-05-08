using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Blind.Models.Models;

namespace Blind.Repositories.MscsRepository
{
	public interface IMscsRepository
	{
		Task<ImageAnalyzeModel> GetImageAnalyze(byte[] stream);
		Task<OcrModel> GetOcr(byte[] stream);
		Task<string> UploadImage(byte[] stream, string endPoint, NameValueCollection queryString);
	}
}