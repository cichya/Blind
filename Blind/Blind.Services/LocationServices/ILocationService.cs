using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blind.Services.LocationServices
{
	public interface ILocationService
	{
		Task<Dictionary<string, double>> GetPosition();
	}
}