using System.Threading.Tasks;

namespace Blind.Services.TextToSpeechServices
{
	public interface ITextToSpeechService
	{
		Task Speak(string text);
	}
}