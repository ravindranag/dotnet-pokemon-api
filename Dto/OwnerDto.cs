using PokemonReviewApp.Models;

namespace PokemonReviewApp.Dto {
	public class OwnerDto {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gym { get; set; }
	}

	public class OwnerCreateDto
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gym { get; set; }
		public int CountryId { get; set; }
	}

	public class OwnerDetailsDto
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gym { get; set; }
		public CountryDto Country { get; set; }
	}
}