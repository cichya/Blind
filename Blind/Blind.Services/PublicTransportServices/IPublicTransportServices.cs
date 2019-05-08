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

namespace Blind.Services.PublicTransportServices
{
	public interface IPublicTransportServices
	{
		Task<BusStopModel> GetBusStops();
		Task<PekaMonitorModel> GetPekaSchedule(string stop);
		Task<JdTransportModel> SetRoute(string fromCoordinate, string toCoordinate);
	}
}