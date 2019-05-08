using System;
using System.Threading.Tasks;
using Stream = System.IO.Stream;

namespace Blind.Services.PhotoService
{
	public interface IPhotoService
	{
		Task GetPhoto();
		event EventHandler PhotoChanged;
		byte[] Photo { get; set; }
	}
}