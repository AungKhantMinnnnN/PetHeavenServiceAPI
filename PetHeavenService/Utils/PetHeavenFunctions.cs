using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using PetHeavenService.Objects;

namespace PetHeavenService.Utils
{
	public class PetHeavenFunctions
	{
		private readonly ILogger<PetHeavenFunctions> _logger;

		public PetHeavenFunctions(ILogger<PetHeavenFunctions> logger)
		{
			logger = _logger;
		}

		public List<Pet> GetAllPets()
		{
			List<Pet> allPets = new List<Pet>();

			using (StreamReader reader = new StreamReader("./DataJson/JsonPetData"))
			{
				string json = reader.ReadToEnd();
				_logger.LogInformation($"JsonData from JsonPetData file: {json}");
                allPets = JsonConvert.DeserializeObject<List<Pet>>(json);
			}

			return allPets;
		}
	}

	
}

