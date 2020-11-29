using System;
using System.Collections.Generic;
using System.Text;

namespace Pokespeare.Models
{
	public class TranslationRequest
	{
		public TranslationRequest(string property, string text)
		{
			Property = property;
			RequestText = text;
		}

		public string Property { get; }
		public string RequestText { get; }
	}
}