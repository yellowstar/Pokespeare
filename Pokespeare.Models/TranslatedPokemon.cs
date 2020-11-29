using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Pokespeare.Models
{
	public class TranslatedPokemon
	{
		public TranslatedPokemon()
		{
		}

		public TranslatedPokemon(string name, List<string> description)
		{
			Name = name;
			Description = description;
		}

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public List<string> Description { get; set; }
	}
}