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
using Blind.Models.Models;
using Blind.Repositories.MscsRepository;

namespace Blind.Services.MSCognitiveServices
{
	public class MscsService:IMscsService
	{
		private IMscsRepository _mscsRepository;

		public MscsService(IMscsRepository mscsRepository)
		{
			this._mscsRepository = mscsRepository;
		}

		public async Task<ImageAnalyzeModel> GetImageAnalyze(byte[] stream)
		{
			return await _mscsRepository.GetImageAnalyze(stream);
		}

		public async Task<OcrModel> GetOcr(byte[] stream)
		{
			return await _mscsRepository.GetOcr(stream);
		}
	}
}