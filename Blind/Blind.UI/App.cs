using Blind.Repositories.MscsRepository;
using Blind.Repositories.PublicTransportRepository;
using Blind.Repositories.TranslationRepository;
using Blind.Services.GeocodingServices;
using Blind.Services.LocationServices;
using Blind.Services.MessageBoxService;
using Blind.Services.MSCognitiveServices;
using Blind.Services.PhotoService;
using Blind.Services.PhotoServices;
using Blind.Services.PublicTransportServices;
using Blind.Services.SerializationService;
using Blind.Services.SerializationServices;
using Blind.Services.SpeechToTextServices;
using Blind.Services.TextToSpeechServices;
using Blind.Services.TranslationServices;
using Blind.ViewModels.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Blind.UI
{
	public class App:MvxApplication
	{
		public App()
		{
			Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<MainViewModel>());

			//if (Mvx.CanResolve<IMvxReachability>())
			//{
			//	int a = 2;
			//}


			Mvx.RegisterType<ISerializationService, SerializationService>();
			Mvx.RegisterType<IMscsRepository, MscsRepository>();
			Mvx.RegisterType<IMscsService, MscsService>();
			Mvx.RegisterType<IPhotoService, PhotoService2>();
			Mvx.RegisterType<IMessageBoxService, MessageBoxService>();
			Mvx.RegisterType<ITextToSpeechService, TextToSpeechService>();
			Mvx.RegisterType<ILocationService, LocationService>();
			Mvx.RegisterType<IGeocodingService, GeocodingService>();
			Mvx.RegisterType<ISpeechToTextService, SpeechToTextService>();
			Mvx.RegisterType<IPublicTransportServices, PublicTransportServices>();
			Mvx.RegisterType<IPublicTransportRepository, PublicTransportRepository>();
			Mvx.RegisterType<ITranslationRepository, TranslationRepository>();
			Mvx.RegisterType<ITranslationService, TranslationService>();
		}
	}
}