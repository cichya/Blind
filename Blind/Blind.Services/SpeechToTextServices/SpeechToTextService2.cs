using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Speech;
using MvvmCross.Platform.Droid.Platform;

namespace Blind.Services.SpeechToTextServices
{
	public class SpeechToTextService2 : ISpeechToTextService
	{
		private IMvxAndroidCurrentTopActivity _activity;

		private bool _isRecording;
		private readonly int VOICE = 10;

		private string _message;

		public const string Tag = "VoiceRec";

		public event EventHandler MessageChanged;

		public string Message
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public SpeechToTextService2(IMvxAndroidCurrentTopActivity activity)
		{
			this._activity = activity;
		}

		public async Task Speak()
		{
			string rec = Android.Content.PM.PackageManager.FeatureMicrophone;
			if (rec != "android.hardware.microphone")
			{
				// no microphone, no recording. Disable the button and output an alert
				throw new Exception();
			}
			else
			{
				_isRecording = !_isRecording;
				if (_isRecording)
				{
					// create the intent and start the activity
					var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
					voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

					// put a message on the modal dialog
					//voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, Application.Context.GetString(Resource.String.messageSpeakNow));

					// if there is more then 1.5s of silence, consider the speech over
					voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
					voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
					voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 1500);
					voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

					// you can specify other languages recognised here, for example
					// voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.German);
					// if you wish it to recognise the default Locale language and German
					// if you do use another locale, regional dialects may not be recognised very well

					voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
					_activity.Activity.StartActivityForResult(voiceIntent, VOICE);

					await StartListening(voiceIntent);
				}
			}
		}

		private async Task StartListening(Intent voiceIntent)
		{
			await Task.Factory.StartNew((() => _activity.Activity.StartActivityForResult(voiceIntent, VOICE)));
		}

		public void OnActivityResult(int requestCode, Result resultVal, Intent data)
		{
			if (requestCode == VOICE)
			{
				if (resultVal == Result.Ok)
				{
					var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
					if (matches.Count != 0)
					{
						_message = matches[0];
					}
					else
					{
						throw new Exception("Nie wykryto ¿adnego polecenia");
					}
				}
			}
		}
	}
}