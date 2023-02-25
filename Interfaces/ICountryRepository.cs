using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface ICountryRepository
	{
		ICollection<CountryDto> GetAllCountries();
		CountryDto GetCountry(int countryId);
		bool CountryExists(int countryId);
		bool CountryExists(string country);
		CountryDto GetCountryByOwner(int ownerId);
		ICollection<OwnerDto> GetOwnersByCountry(int countryId);
		bool CreateCountry(Country country);
		bool Save();
	}
}