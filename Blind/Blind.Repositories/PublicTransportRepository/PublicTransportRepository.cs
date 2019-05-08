using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Blind.Models.Models;
using Newtonsoft.Json;

namespace Blind.Repositories.PublicTransportRepository
{
	public class PublicTransportRepository: IPublicTransportRepository
	{
		public async Task<PekaMonitorModel> GetPekaSchedule(string stop)
		{
			string uri = @"https://www.peka.poznan.pl/vm/method.vm?ts=" + JavaScriptGetTime();

			using (var client = new HttpClient())
			{
				PekaParameterModel pekaParameter = new PekaParameterModel()
				{
					symbol = stop
				};

				var p0Parameter = JsonConvert.SerializeObject(pekaParameter);

				var parameters = new FormUrlEncodedContent(new[]
				{
					new KeyValuePair<string, string>("method", "getTimes"),
					new KeyValuePair<string, string>("p0", p0Parameter)
				});

				var resposne = await client.PostAsync(uri, parameters);

				var result = await resposne.Content.ReadAsStringAsync();

				return JsonConvert.DeserializeObject<PekaMonitorModel>(result);
			}
		}

		public async Task<BusStopModel> GetBusStops()
		{
			string uri = @"http://www.poznan.pl/mim/plan/map_service.html?mtype=pub_transport&co=cluster";

			using (var client = new HttpClient())
			{
				var response = await client.GetAsync(uri);

				var result = await response.Content.ReadAsStringAsync();

				return JsonConvert.DeserializeObject<BusStopModel>(result);
			}
		}

		public async Task<JdTransportModel> SetRoute(string fromCoordinate, string toCoordinate)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("X-jd-param-profile-login", "jda-ba9fe87c-3939-4a76-8072-dd8c5134df71n9O15bbsP4fEW4f21a19");
				client.DefaultRequestHeaders.Add("X-jd-param-user-device-id", "1493560304357_0.24297172828324076_jakdojade");
				client.DefaultRequestHeaders.Add("Cookie", "_ga=GA1.2.1025189326.1493560304; __gfp_64b=sfDQIEp1X_s_9XJ4q5GDGEk2aChKF8snQw4bVE3r9i7.F7; G_ENABLED_IDPS=google; ea_uuid=201704301551448881300797; _gat=1");

				string url = "https://jakdojade.pl/api/web/v1/routes?fc=52.39694:16.94559&tc=52.40203:16.91189&fsn=Serafitek&tsn=POZNA%C5%83%20G%C5%81%C3%93WNY&fsc=&tsc=&time=30.04.17%2017:52&ia=&t=convenient&aac=true&aab=&act=2&apv=&aax=&aaz=&aol=&aro=0&apl=&apo=&ri=1&rc=3&rt=false&alt=0";

				var result = await client.GetStringAsync(url);

				return JsonConvert.DeserializeObject<JdTransportModel>(result);
			}
		}

		private long JavaScriptGetTime()
		{
			long retval = 0;
			var st = new DateTime(1970, 1, 1);
			TimeSpan t = (DateTime.Now.ToUniversalTime() - st);
			retval = (Int64)(t.TotalMilliseconds + 0.5);
			return retval;
		}
	}
}