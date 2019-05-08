using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Android.Graphics;
using Blind.Models.Models;
using Blind.Services.GeocodingServices;
using Blind.Services.LocationServices;
using Blind.Services.MessageBoxService;
using Blind.Services.MSCognitiveServices;
using Blind.Services.PhotoService;
using Blind.Services.PublicTransportServices;
using Blind.Services.SerializationService;
using Blind.Services.SettingsServices;
using Blind.Services.SpeechToTextServices;
using Blind.Services.TextToSpeechServices;
using Blind.Services.TranslationServices;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Droid.Platform;

namespace Blind.ViewModels.ViewModels
{
	public class MainViewModel : MvxViewModel
	{
		private string _message = "abc";
		private string _menu = "abc";

		#region TextOrder

		private const string TakePictureOfSurroundingsOrder = "co widzê";
		private const string LocationOrder = "gdzie jestem";
		private const string ReadTextOrder = "przeczytaj tekst";
		private const string WhenBusTextOrder = "kiedy tramwaj";
		private const string SetRouteTextOrder = "wyznacz trasê";
		private const string BanknoteDenominationTextOrder = "sprawdŸ banknot";
		private const string MenuTextOrder = "menu";

		#endregion

		#region ButtonVisibility

		private bool _whatISeeButtonVisible;
		private bool _ocrReaderButtonVisible;
		private bool _getBanknoteDenominationButtonVisible;

		#endregion

		private BusStopModel _busStopsList;

		#region Properties

		public Bitmap MyImage
		{
			get { return myImage; }
			set
			{
				myImage = value;
				RaisePropertyChanged(() => MyImage);
			}
		}

		public bool GetBanknoteDenominationButtonVisible
		{
			get { return _getBanknoteDenominationButtonVisible; }
			set
			{
				_getBanknoteDenominationButtonVisible = value;
				RaisePropertyChanged(() => GetBanknoteDenominationButtonVisible);
			}
		}

		public bool WhatISeeButtonVisible
		{
			get { return _whatISeeButtonVisible; }
			set
			{
				_whatISeeButtonVisible = value;
				RaisePropertyChanged(() => WhatISeeButtonVisible);
			}
		}

		public bool OcrReaderButtonVisible
		{
			get { return _ocrReaderButtonVisible; }
			set
			{
				_ocrReaderButtonVisible = value;
				RaisePropertyChanged(() => OcrReaderButtonVisible);
			}
		}

		public string Message
		{
			get { return _message; }
			set
			{
				_message = value;
				RaisePropertyChanged(() => Message);
			}
		}

		public string Menu
		{
			get { return _menu; }
			set
			{
				_menu = value;
				RaisePropertyChanged(() => Menu);
			}
		}

		#endregion

		#region Constructor and services

		private IMscsService _mscsService;
		private IPhotoService _photoService;
		private IMessageBoxService _messageBoxService;
		private ITextToSpeechService _textToSpeechService;
		private ILocationService _locationService;
		private IGeocodingService _geocodingService;
		private ISpeechToTextService _speechToTextService;
		private IPublicTransportServices _publicTransportServices;
		private ITranslationService _translationService;
		private ISettingsService _checkNetwork;
		private ISettingsService _checkLocation;

		public MainViewModel(IMscsService mscsService, ISerializationService serializationService,
			IPhotoService photoService, IMessageBoxService messageBoxService,
			ITextToSpeechService textToSpeechService, ILocationService locationService,
			IGeocodingService geocodingService, ISpeechToTextService speechToTextService,
			IPublicTransportServices publicTransportServices,
			ITranslationService translationService,
			IMvxAndroidCurrentTopActivity activity)
		{
			this._mscsService = mscsService;
			this._photoService = photoService;
			this._messageBoxService = messageBoxService;
			this._textToSpeechService = textToSpeechService;
			this._locationService = locationService;
			this._geocodingService = geocodingService;
			this._speechToTextService = speechToTextService;
			this._publicTransportServices = publicTransportServices;
			this._translationService = translationService;

			this._checkNetwork = new CheckInternetConnection();
			this._checkLocation = new CheckLocationAvailability();

			this._speechToTextService.MessageChanged += HandleMessageChanged;
			this._photoService.PhotoChanged += HandlePhotoChanged;

			_textToSpeechService.Speak(
				"Witaj w aplikacji blind. Tapnij na ekran i powiedz komende dla danej funkcji. Aby uzyskaæ informacjê o dostêpnych funkcjach tapnij w ekran i powiedz menu");

			CheckSettingsAfterStart();
		}

		#endregion

		#region Commands

		private MvxCommand _getBanknoteDenominationCommand;
		private MvxCommand _whatISeeCommand;
		private MvxCommand _readTextCommand;
		private MvxCommand _getLocationCommand;
		private MvxCommand _geocodingCommand;
		private MvxCommand _speechToTextCommand;
		private MvxCommand _getBusStopsCommand;
		private MvxCommand _getPekaMonitorCommand;
		private MvxCommand _getTranslateCommand;
		private MvxCommand _checkSettingsCommand;

		public MvxCommand GetBanknoteDenominationCommand
		{
			get
			{
				return _getBanknoteDenominationCommand ??
					   (_getBanknoteDenominationCommand = new MvxCommand(GetBanknoteDenomination2));
			}
		}

		public MvxCommand WhatISeeCommand
		{
			get
			{
				return _whatISeeCommand ??
					   (_whatISeeCommand = new MvxCommand(WhatISee));
			}
		}

		public MvxCommand ReadTextCommand
		{
			get
			{
				return _readTextCommand ??
					   (_readTextCommand = new MvxCommand(ReadText));
			}
		}

		public MvxCommand GetLocationCommand
		{
			get
			{
				return _getLocationCommand ??
					   (_getLocationCommand = new MvxCommand(GetLocationCoordinate));
			}
		}

		public MvxCommand GeocodingCommand
		{
			get
			{
				return _geocodingCommand ??
					   (_geocodingCommand = new MvxCommand(Geocoding));
			}
		}

		public MvxCommand SpeechToTextCommand
		{
			get
			{
				return _speechToTextCommand ??
					   (_speechToTextCommand = new MvxCommand(SpeechToText));
			}
		}

		public MvxCommand GetBusStopsCommand
		{
			get
			{
				return _getBusStopsCommand ??
					   (_getBusStopsCommand = new MvxCommand(GetBusStops));
			}
		}

		public MvxCommand GetPekaMonitorCommand
		{
			get
			{
				return _getPekaMonitorCommand ??
					   (_getPekaMonitorCommand = new MvxCommand(GetPekaMonitor));
			}
		}

		public MvxCommand GetTranslateCommand
		{
			get
			{
				return _getTranslateCommand ??
					   (_getTranslateCommand = new MvxCommand(Translate));
			}
		}

		public MvxCommand CheckSettingsCommand
		{
			get
			{
				return _checkSettingsCommand ??
					   (_checkSettingsCommand = new MvxCommand(CheckSettingsAfterStart));
			}
		}

		#endregion

		#region TakePhoto

		private async Task TakePhoto()
		{
			await _photoService.GetPhoto();
		}

		#endregion

		#region HelperMethods

		private async void GetLocationCoordinate()
		{
			var result = await _locationService.GetPosition();
		}

		private string OcrFormat(OcrModel ocrModel)
		{
			return (from reg in ocrModel.regions
					from line in reg.lines
					from word in line.words
					select word)
				.Aggregate(String.Empty, (current, word) => current + (word.text + " "));
		}

		private string GetBanknoteDenomination(OcrModel ocrModel)
		{
			string result = string.Empty;

			foreach (var reg in ocrModel.regions)
			{
				foreach (var line in reg.lines)
				{
					foreach (var word in line.words)
					{


						result += word.text + " ";
					}
				}
			}
			return result;
		}

		private async void GetBusStops()
		{
			var model = await _publicTransportServices.SetRoute("", "");
			//BusStopModel model = await _publicTransportServices.GetBusStops();
		}

		private async void GetPekaMonitor()
		{
			PekaMonitorModel model = await _publicTransportServices.GetPekaSchedule("AWF41");
		}

		private async void Translate()
		{
			var result = await _translationService.Translate("a beautiful big woman");
		}

		#endregion

		#region Location

		private async void Geocoding()
		{
			try
			{
				if (!CheckSettings(SettingsToCheck.Localization))
				{
					await _textToSpeechService.Speak("Do poprawnego dzia³ania aplikacji potrzebna jest aktywna lokalizacja");
					return;
				}

				await InProgress();

				var location = await _locationService.GetPosition();

				var result = await _geocodingService.Geocoding(location["Latitude"], location["Longitude"]);

				var result2 = await _geocodingService.ReverseGeocoding(result);

				result = await _geocodingService.Geocoding(result2["Latitude"], result2["Longitude"]);

				await _textToSpeechService.Speak(result);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		#endregion

		#region SpeechToText

		private async void SpeechToText()
		{
			await _speechToTextService.Speak();
		}

		#endregion

		#region Settings

		private enum SettingsToCheck
		{
			Internet,
			Localization,
			All,
		}

		private bool CheckSettings(SettingsToCheck toCheck)
		{
			if (toCheck == SettingsToCheck.Internet)
			{
				return _checkNetwork.Check();
			}
			else if (toCheck == SettingsToCheck.Localization)
			{
				return _checkLocation.Check();
			}
			else
			{
				return _checkNetwork.Check() && _checkLocation.Check();
			}
		}

		private async void CheckSettingsAfterStart()
		{
			bool isInternet = CheckSettings(SettingsToCheck.Internet);
			bool isLocalization = CheckSettings(SettingsToCheck.Localization);

			if (!isInternet && !isLocalization)
			{
				await
					_textToSpeechService.Speak(
						"Do poprawnego dzia³ania aplikacji potrzebne jest po³¹czenie z internetem oraz w³¹czona lokalizacja");
				return;
			}
			if (!isInternet)
			{
				await _textToSpeechService.Speak("Do poprawnego dzia³ania aplikacji potrzebne jest po³¹czenie z internetem");
				return;
			}
			if (!isLocalization)
			{
				await _textToSpeechService.Speak("Do poprawnego dzia³ania aplikacji potrzebna jest aktywna lokalizacja");
				return;
			}
		}

		#endregion

		#region Main

		private async void HandleMessageChanged(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(_speechToTextService.Message))
				{
					throw new Exception("Nie wykryto ¿adnego polecenia");
				}

				if (_whenBusModeFrom)
				{
					if (!CheckSettings(SettingsToCheck.Internet))
					{
						throw new Exception("Do poprawnego dzia³ania tej funkcji potrzebne jest po³¹czenie z internetem");
					}

					_whenBusModeFrom = false;

					GetBusStop(_speechToTextService.Message);

					if (_busStopNames == null)
					{
						await _textToSpeechService.Speak("Nie znaleziono przystanku o takiej nazwie");
						return;
					}

					await _textToSpeechService.Speak("Podaj numer tramwaju lub autobusu");

					_whenBusModeNumber = true;

					await Task.Delay(3000);

					await _speechToTextService.Speak();

					return;
				}

				if (_whenBusModeNumber)
				{
					_whenBusModeNumber = false;

					int.TryParse(_speechToTextService.Message, out _busNumber);

					if (_busNumber == 0)
					{
						await _textToSpeechService.Speak("Nie mo¿na odnaleŸæ pojazdu o takim numerze");
						return;
					}

					await _textToSpeechService.Speak("Podaj kierunek");

					_whenBusModeTo = true;

					await Task.Delay(2000);

					await _speechToTextService.Speak();

					return;
				}

				if (_whenBusModeTo)
				{
					_whenBusModeTo = false;

					GetTravelDirection(_speechToTextService.Message);

					if (_busDirection == null)
					{
						await _textToSpeechService.Speak("Nie mo¿na wprowadzonych danych z ¿adnym tramwajem lub autobusem");
						return;
					}

					CheckPekaMonitor();

					return;
				}

				if (_setRouteMode)
				{
					if (!CheckSettings(SettingsToCheck.All))
					{
						throw new Exception(
							"Do poprawnego dzia³ania aplikacji potrzebne jest po³¹czenie z internetem oraz w³¹czona lokalizacja");
					}

					_setRouteMode = false;

					if (string.IsNullOrEmpty(_speechToTextService.Message))
					{
						await _textToSpeechService.Speak("Musisz podaæ adres docelowy");
						return;
					}

					await SetRoute2(_speechToTextService.Message);

					return;
				}

				switch (_speechToTextService.Message.ToLower())
				{
					case MenuTextOrder:
						SpeakHint();
						break;
					case TakePictureOfSurroundingsOrder:
						TakePictureOfSurroundings();
						break;
					case LocationOrder:
						Geocoding();
						break;
					case ReadTextOrder:
						OcrReader();
						break;
					case WhenBusTextOrder:
						WhenBus();
						break;
					case SetRouteTextOrder:
						SetRoute();
						break;
					case BanknoteDenominationTextOrder:
						GetBanknoteDenomination();
						break;
					default:
						await _textToSpeechService.Speak("Brak takiej funkcji");
						break;
				}
			}
			catch (Exception exception)
			{
				_whenBusModeFrom = false;
				_whenBusModeNumber = false;
				_whenBusModeTo = false;
				_setRouteMode = false;
				await _textToSpeechService.Speak(exception.Message);
			}
		}

		private void HandlePhotoChanged(object sender, EventArgs e)
		{
			if (_whatISeeMode)
			{
				_whatISeeMode = false;
				WhatISee2();
				return;
			}

			if (_ocrMode)
			{
				_ocrMode = false;
				ReadText2();
				return;
			}

			if (_getBanknoteDenominationMode)
			{
				_getBanknoteDenominationMode = false;
				GetBanknoteDenomination3();
				return;
			}
		}

		#endregion

		#region WhatISee

		private bool _whatISeeMode = false;

		private async void TakePictureOfSurroundings()
		{
			if (!CheckSettings(SettingsToCheck.Internet))
			{
				await _textToSpeechService.Speak("Do poprawnego dzia³ania tej funkcji potrzebne jest po³¹czenie z internetem");
				return;
			}

			WhatISeeButtonVisible = true;
			await _textToSpeechService.Speak("Skieruj aparat w interesuj¹ce ciê miejsce i wciœnij ekran telefonu");
		}

		private async void WhatISee()
		{
			try
			{
				_whatISeeMode = true;

				await TakePhoto();
			}
			catch (Exception ex)
			{
				_messageBoxService.ShowErrorMessageBox(ex.Message);
				_whatISeeMode = false;
			}
			finally
			{
				WhatISeeButtonVisible = false;
			}
		}

		private async void WhatISee2()
		{
			try
			{
				if (!CheckSettings(SettingsToCheck.Internet))
				{
					throw new Exception("Do poprawnego dzia³ania tej funkcji potrzebne jest po³¹czenie z internetem");
				}

				await InProgress();

				var result = await _mscsService.GetImageAnalyze(_photoService.Photo);

				MyImage = BitmapFactory.DecodeByteArray(_photoService.Photo, 0, _photoService.Photo.Length);

				var message = result.description.captions[0].text;

				message = await _translationService.Translate(message);

				await _textToSpeechService.Speak(message);
			}
			catch (Exception e)
			{

				throw e;
			}
		}

		#endregion

		#region GetBanknoteDenomination

		private bool _getBanknoteDenominationMode = false;

		private async void GetBanknoteDenomination()
		{
			if (!CheckSettings(SettingsToCheck.Internet))
			{
				await _textToSpeechService.Speak("Do poprawnego dzia³ania tej funkcji potrzebne jest po³¹czenie z internetem");
				return;
			}

			GetBanknoteDenominationButtonVisible = true;
			await _textToSpeechService.Speak("Skieruj aparat w interesuj¹ce ciê miejsce i wciœnij ekran telefonu");
		}

		private async void GetBanknoteDenomination2()
		{
			try
			{
				_getBanknoteDenominationMode = true;
				await TakePhoto();
			}
			catch (Exception e)
			{
				_getBanknoteDenominationMode = false;
			}
			finally
			{
				GetBanknoteDenominationButtonVisible = false;
			}
		}

		private async void GetBanknoteDenomination3()
		{
			try
			{
				if (!CheckSettings(SettingsToCheck.Internet))
				{
					throw new Exception("Do poprawnego dzia³ania tej funkcji potrzebne jest po³¹czenie z internetem");
				}

				var text = await _mscsService.GetOcr(_photoService.Photo);

				MyImage = BitmapFactory.DecodeByteArray(_photoService.Photo, 0, _photoService.Photo.Length);

				var message = (from reg in text.regions
							   from line in reg.lines
							   from word in line.words
							   select word)
					.Aggregate(String.Empty, (current, word) => current + (word.text + " "));

				await _textToSpeechService.Speak(message);
			}
			catch (Exception e)
			{

				throw e;
			}
		}

		#endregion

		private Bitmap myImage;

		#region OCR

		private bool _ocrMode = false;

		private async void OcrReader()
		{
			if (!CheckSettings(SettingsToCheck.Internet))
			{
				await _textToSpeechService.Speak("Do poprawnego dzia³ania tej funkcji potrzebne jest po³¹czenie z internetem");
				return;
			}

			OcrReaderButtonVisible = true;
			await _textToSpeechService.Speak("Skieruj aparat w interesuj¹ce ciê miejsce i wciœnij ekran telefonu");
		}

		private async void ReadText()
		{
			try
			{
				_ocrMode = true;

				await TakePhoto();
			}
			catch (Exception e)
			{
				_ocrMode = false;
			}
			finally
			{
				OcrReaderButtonVisible = false;
			}
		}

		private async void ReadText2()
		{
			try
			{
				if (!CheckSettings(SettingsToCheck.Internet))
				{
					throw new Exception("Do poprawnego dzia³ania tej funkcji potrzebne jest po³¹czenie z internetem");
				}

				await InProgress();

				var text = await _mscsService.GetOcr(_photoService.Photo);

				MyImage = BitmapFactory.DecodeByteArray(_photoService.Photo, 0, _photoService.Photo.Length);

				var message = (from reg in text.regions
							   from line in reg.lines
							   from word in line.words
							   select word)
					.Aggregate(String.Empty, (current, word) => current + (word.text + " "));

				await _textToSpeechService.Speak(message);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		#endregion

		#region SetRoute

		private bool _setRouteMode = false;

		private async void SetRoute()
		{
			if (!CheckSettings(SettingsToCheck.All))
			{
				await _textToSpeechService.Speak(
					"Do poprawnego dzia³ania aplikacji potrzebne jest po³¹czenie z internetem oraz w³¹czona lokalizacja");
				return;
			}

			await _textToSpeechService.Speak("Podaj adres");

			_setRouteMode = true;

			await Task.Delay(2000);

			await _speechToTextService.Speak();
		}

		private async Task SetRoute2(string address)
		{
			var myPosition = await _locationService.GetPosition();

			string fromCoordinate = $"{myPosition["Latitude"]}:{myPosition["Longitude"]}";

			//string address = message;

			var destinationPosition = await _geocodingService.ReverseGeocoding(address);

			string toCoordinate = $"{destinationPosition["Latitude"]}:{destinationPosition["Longitude"]}";

			var result = await _publicTransportServices.SetRoute(fromCoordinate, toCoordinate);
		}

		#endregion

		#region Peka

		private bool _whenBusModeTo;

		private bool _whenBusModeNumber;

		private bool _whenBusModeFrom;

		List<string> _busStopNames;

		private int _busNumber;

		List<string> _busDirection;

		private void GetBusStop(string busStopName)
		{
			if (busStopName == null)
				return;

			_busStopNames = new List<string>();

			_busStopNames = (from b in _busStopsList.features
							 where b.properties.stop_name == busStopName
							 select b.id).ToList();

			if (_busStopNames.Count == 0)
				_busStopNames = null;
		}

		private async void WhenBus()
		{
			if (_busStopsList == null)
			{
				if (!CheckSettings(SettingsToCheck.Internet))
				{
					await _textToSpeechService.Speak("Do poprawnego dzia³ania tej funkcji potrzebne jest po³¹czenie z internetem");
					return;
				}

				_busStopsList = await _publicTransportServices.GetBusStops();
			}

			await _textToSpeechService.Speak("Podaj nazwê przystanku");

			_whenBusModeFrom = true;

			await Task.Delay(2000);

			await _speechToTextService.Speak();
		}

		private async void CheckPekaMonitor()
		{
			await InProgress();

			foreach (var stopName in _busStopNames)
			{
				var peka = await _publicTransportServices.GetPekaSchedule(stopName);

				var query = peka.success.times.FirstOrDefault(e => e.line == _busNumber.ToString() &&
																   string.Equals(e.direction, _busDirection.FirstOrDefault(),
																	   StringComparison.CurrentCultureIgnoreCase));

				if (query == null)
					continue;

				await _textToSpeechService.Speak($"pojazd przyjedzie za {query.minutes} minut");
				return;
			}
		}

		private void GetTravelDirection(string direction)
		{
			if (string.IsNullOrEmpty(direction))
			{
				_busDirection = null;
				return;
			}

			MatchCollection matches = Regex.Matches(direction, @"\b[\w']*\b");

			var words = from m in matches.Cast<Match>()
						where !string.IsNullOrEmpty(m.Value)
						select TrimSuffix(m.Value);

			_busDirection = new List<string>();

			_busDirection = words.ToList();

			if (_busDirection.Count == 0)
			{
				_busDirection = null;
				return;
			}
		}

		static string TrimSuffix(string word)
		{
			int apostropheLocation = word.IndexOf('\'');
			if (apostropheLocation != -1)
			{
				word = word.Substring(0, apostropheLocation);
			}

			return word;
		}

		#endregion

		#region Hint

		private async void SpeakHint()
		{
			await _textToSpeechService.Speak("Aby dowiedzieæ siê co jest wokó³ ciebie powiedz co widzê." +
											 "Aby dowiedzieæ siê gdzie siê znajdujesz powiedz gdzie jestem." +
											 "Aby zeskanowaæ treœæ dokumentu powiedz przeczytaj tekst." +
											 "Aby dowiedzieæ siê kiedy przyjedzie tramwaj powiedz kiedy tramwaj." +
											 "Aby wyznaczyæ trasê jazdy komunikacj¹ miejsk¹ powiedz wyznacz trasê");
		}

		#endregion

		private async Task InProgress()
		{
			await _textToSpeechService.Speak("Przetwarzanie");
			await Task.Delay(1000);
		}
	}
}