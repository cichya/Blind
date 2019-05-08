using System.Collections.Generic;
using Newtonsoft.Json;

namespace Blind.Models.Models
{
	public class JdTransportModel
	{
		public string cityName { get; set; }
		public string citySymbol { get; set; }
		public List<Route> routes { get; set; }
	}

	public class Line1
	{
		public string lineId { get; set; }
		public string lineName { get; set; }
		public string lineVehicleType { get; set; }
	}

	public class TransportOperator
	{
		public int operatorId { get; set; }
		public string operatorSymbol { get; set; }
		public string operatorName { get; set; }
	}

	public class StopPointCoordinate
	{
		public double x_lon { get; set; }
		public double y_lat { get; set; }
	}

	public class StopPoint
	{
		public string stopName { get; set; }
		public string stopPointCode { get; set; }
		public string stopPointStreetName { get; set; }
		public StopPointCoordinate stopPointCoordinate { get; set; }
	}

	public class Stop
	{
		public StopPoint stopPoint { get; set; }
		public string lineStopDynamicId { get; set; }
		public List<object> shape { get; set; }
		public int stopZoneCode { get; set; }
		public string stopZoneName { get; set; }
		public string departureTimeSchedule { get; set; }
		public string arrivalTimeSchedule { get; set; }
	}

	public class Stops
	{
		public int startIndex { get; set; }
		public int endIndex { get; set; }
		public List<Stop> stops { get; set; }
	}

	public class Line2
	{
		public string lineId { get; set; }
		public string lineName { get; set; }
		public string lineVehicleType { get; set; }
	}

	public class TransportOperator2
	{
		public int operatorId { get; set; }
		public string operatorSymbol { get; set; }
		public string operatorName { get; set; }
	}

	public class AlternativeLine
	{
		public Line2 line { get; set; }
		public string lineHeadingText { get; set; }
		public TransportOperator2 transportOperator { get; set; }
		public bool notMainVariant { get; set; }
	}

	public class RouteLine
	{
		[JsonProperty("line")]
		public Line1 line { get; set; }
		public string lineHeadingText { get; set; }
		public TransportOperator transportOperator { get; set; }
		public bool notMainVariant { get; set; }
		public int departuresPeriodMinutes { get; set; }
		public string courseId { get; set; }
		public string realtimeStatus { get; set; }
		public Stops stops { get; set; }
		public List<AlternativeLine> alternativeLines { get; set; }
	}

	public class RoutePart
	{
		public string routePartType { get; set; }
		public RouteLine routeLine { get; set; }
		public string routePartStartDepartureTimeSchedule { get; set; }
		public string routePartTargetArrivalTimeSchedule { get; set; }
	}

	public class RouteTicket
	{
		public int ticketPriceCents { get; set; }
		public string ticketDescription { get; set; }
		public string ticketPriceCurrencySymbol { get; set; }
	}

	public class Route
	{
		public string routeRealtimeStatus { get; set; }
		public List<RoutePart> routeParts { get; set; }
		public List<RouteTicket> routeTickets { get; set; }
		public string routeLink { get; set; }
	}

}