using System;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Speech;
using Android.Util;
using MvvmCross.Platform.Droid.Platform;

namespace Blind.Services.SpeechToTextServices
{
	public class SpeechToTextService: Java.Lang.Object, ISpeechToTextService, IRecognitionListener
	{
		private IMvxAndroidCurrentTopActivity _activity;

		private SpeechRecognizer Recognizer { get; set; }
		private Intent SpeechIntent { get; set; }
		private string _message;

		public string Message
		{
			get { return _message; }
			set
			{
				_message = value;
				MessageChanged?.Invoke(this, null);
			}
		}

		public event EventHandler MessageChanged;

		public const string Tag = "Voice Recording";

		public SpeechToTextService(IMvxAndroidCurrentTopActivity activity)
		{
			this._activity = activity;
		}

		public async Task Speak()
		{
			Recognizer = SpeechRecognizer.CreateSpeechRecognizer(_activity.Activity);
			Recognizer.SetRecognitionListener(this);

			SpeechIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);

			//SpeechIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
			//SpeechIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
			//SpeechIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 1500);
			SpeechIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 2);

			SpeechIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
			SpeechIntent.PutExtra(RecognizerIntent.ExtraCallingPackage, _activity.Activity.PackageName);

			await StartListening();
		}

		private async Task StartListening()
		{
			_activity.Activity.RunOnUiThread((() => Recognizer.StartListening(SpeechIntent)));
		}

		public void OnBeginningOfSpeech()
		{
			Log.Debug(Tag, "On Beginning of Speech");
		}

		public void OnBufferReceived(byte[] buffer)
		{
			//throw new NotImplementedException();
		}

		public void OnEndOfSpeech()
		{
			Log.Debug(Tag, "On end of Speech");
		}

		public void OnError(SpeechRecognizerError error)
		{
			Log.Debug("On Error", error.ToString());

			if (error == SpeechRecognizerError.NoMatch)
			{
				Message = null;
			}
		}

		public void OnEvent(int eventType, Bundle @params)
		{
			//throw new NotImplementedException();
		}

		public void OnPartialResults(Bundle partialResults)
		{
			//throw new NotImplementedException();
		}

		public void OnReadyForSpeech(Bundle @params)
		{
			Log.Debug(Tag, "On Ready for Speech");
		}

		public void OnResults(Bundle results)
		{
			var matches = results.GetStringArrayList(SpeechRecognizer.ResultsRecognition);
			if (matches != null && matches.Count > 0)
			{
				Message = matches[0];
			}
		}

		public void OnRmsChanged(float rmsdB)
		{
			//throw new NotImplementedException();
		}
	}
}