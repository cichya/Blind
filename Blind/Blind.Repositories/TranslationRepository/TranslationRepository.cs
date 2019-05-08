using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Blind.Repositories.TranslationRepository
{
	public class TranslationRepository:ITranslationRepository
	{
		public async Task<string> Translate(string text)
		{
			var url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl=en&tl=pl&dt=t&q=" + HttpUtility.UrlEncode(text);

			using (var client = new HttpClient())
			{
				var response = await client.GetAsync(url);

				var result = await response.Content.ReadAsStringAsync();

				int lastIndex = result.IndexOf('"', result.IndexOf('"') + 1);

				return result.Substring(4, lastIndex - 4);
			}
		}
	}
}