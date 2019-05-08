using System;
using System.Threading.Tasks;

namespace Blind.Services.SpeechToTextServices
{
	public interface ISpeechToTextService
	{
		event EventHandler MessageChanged;
		string Message { get; set; }
		Task Speak();
	}
}