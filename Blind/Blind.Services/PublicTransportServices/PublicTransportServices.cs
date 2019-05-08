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
using Blind.Repositories.PublicTransportRepository;

namespace Blind.Services.PublicTransportServices
{
	public class PublicTransportServices : IPublicTransportServices
	{
		private IPublicTransportRepository _publicTransportRepository;

		public PublicTransportServices(IPublicTransportRepository publicTransportRepository)
		{
			this._publicTransportRepository = publicTransportRepository;
		}

		public async Task<PekaMonitorModel> GetPekaSchedule(string stop)
		{
			return await _publicTransportRepository.GetPekaSchedule(stop);
		}

		public async Task<BusStopModel> GetBusStops()
		{
			return await _publicTransportRepository.GetBusStops();
		}

		public async Task<JdTransportModel> SetRoute(string fromCoordinate, string toCoordinate)
		{
			return await _publicTransportRepository.SetRoute(fromCoordinate, toCoordinate);
		}
	}
}