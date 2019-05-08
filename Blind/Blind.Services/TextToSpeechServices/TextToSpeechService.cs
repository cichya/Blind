using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Speech.Tts;
using Android.Util;
using MvvmCross.Platform.Droid.Platform;

namespace Blind.Services.TextToSpeechServices
{
	public class TextToSpeechService : Java.Lang.Object, ITextToSpeechService, TextToSpeech.IOnInitListener
	{
		private IMvxAndroidCurrentTopActivity _activity;

		private TextToSpeech _speaker;
		private string _toSpeak;


		public TextToSpeechService(IMvxAndroidCurrentTopActivity activity)
		{
			this._activity = activity;
		}

		public async Task Speak(string text)
		{
			_toSpeak = text;

			if (_speaker == null)
			{
				_speaker = new TextToSpeech(_activity.Activity, this);
			}
			else
			{
				var p = new Dictionary<string, string>();
				//_speaker.Speak(_toSpeak, QueueMode.Flush, p);
				await DoSpeak(p);
			}
		}

		private async Task DoSpeak(Dictionary<string, string> p)
		{
			//_activity.Activity.RunOnUiThread((() => _speaker.Speak(_toSpeak, QueueMode.Flush, p)));
			await Task.Factory.StartNew((() => _speaker.Speak(_toSpeak, QueueMode.Flush, p)));
		}

		public void OnInit(OperationResult status)
		{
			if (status.Equals(OperationResult.Success))
			{
				var p = new Dictionary<string, string>();
				_speaker.Speak(_toSpeak, QueueMode.Flush, p);
			}
		}
	}
}