using PokemonReviewApp.Dto;

namespace PokemonReviewApp.Interfaces
{
	public interface ICountryRepository
	{
		ICollection<CountryDto> GetAllCountries();
		CountryDto GetCountry(int countryId);
		bool CountryExists(int countryId);
		CountryDto GetCountryByOwner(int ownerId);
		ICollection<OwnerDto> GetOwnersByCountry(int countryId);
	}
}