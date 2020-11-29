using System;
using System.Collections.Generic;
using System.Text;

namespace Pokespeare.Models
{
	public class TranslationResponse
	{
		public TranslationResponse(string property, string text)
		{
			Property = property;
			TranslatedText = text;
		}

		public string Property { get; }
		public string TranslatedText { get; }
	}
}