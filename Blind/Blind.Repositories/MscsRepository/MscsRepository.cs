using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Blind.Models.Models;
using Newtonsoft.Json;

namespace Blind.Repositories.MscsRepository
{
	public class MscsRepository:IMscsRepository
	{
		private const string uriRepository = "https://westus.api.cognitive.microsoft.com/vision/v1.0/";

		public async Task<ImageAnalyzeModel> GetImageAnalyze(byte[] stream)
		{
			string endPoint = "analyze?";

			var queryString = HttpUtility.ParseQueryString(string.Empty);
			queryString["visualFeatures"] = "Description";
	
			var result = await UploadImage(stream, endPoint, queryString);

			return JsonConvert.DeserializeObject<ImageAnalyzeModel>(result);
		}

		public async Task<OcrModel> GetOcr(byte[] stream)
		{
			string endPoint = "ocr?";
			var queryString = HttpUtility.ParseQueryString(string.Empty);
			queryString["language=unk&detectOrientation"] = "true";

			var result = await UploadImage(stream, endPoint, queryString);

			return JsonConvert.DeserializeObject<OcrModel>(result);
		}

		public async Task<string> UploadImage(byte[] stream, string endPoint, NameValueCollection queryString)
		{
			var uri = uriRepository + endPoint + queryString;

			using(var client = new HttpClient())
			using (var content = new ByteArrayContent(stream))
			{
				client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "key");

				content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

				var response = await client.PostAsync(uri, content);

				return await response.Content.ReadAsStringAsync();
			}
		}
	}
}