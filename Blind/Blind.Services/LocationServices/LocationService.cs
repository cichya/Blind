using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Geolocator;

namespace Blind.Services.LocationServices
{
	public class LocationService:ILocationService
	{
		public async Task<Dictionary<string, double>> GetPosition()
		{
			var locator = CrossGeolocator.Current;

			var result = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

			return new Dictionary<string, double>()
			{
				{"Latitude", result.Latitude },
				{"Longitude", result.Longitude}
			};
		}
	}
}