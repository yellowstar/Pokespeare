using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokespeare.Models
{
	public class ApiTranslationResponse
	{
		public Success success { get; set; }
		public Contents contents { get; set; }
	}

	[JsonObject("success")]
	public class Success
	{
		public int total { get; set; }
	}

	[JsonObject("contents")]
	public class Contents
	{
		public string translated { get; set; }
		public string text { get; set; }
		public string translation { get; set; }
	}
}