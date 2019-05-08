using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Blind.Repositories.TranslationRepository;

namespace Blind.Services.TranslationServices
{
	public class TranslationService:ITranslationService
	{
		private ITranslationRepository _translationRepository;

		public TranslationService(ITranslationRepository translationRepository)
		{
			_translationRepository = translationRepository;
		}

		public async Task<string> Translate(string text)
		{
			return await _translationRepository.Translate(text);
		}
	}
}