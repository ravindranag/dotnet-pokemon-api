using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IOwnerRepository
	{
		ICollection<OwnerDto> GetAllOwners();
		OwnerDetailsDto GetOwnerById(int ownerId);
		ICollection<PokemonDto> GetPokemonByOwner(int ownerId);
		OwnerDto GetOwnerByPokemon(int pokeId);
		bool OwnerExists(int ownerId);
		bool OwnerExists(string firstName, string lastName);
		bool CreateOwner(Owner owner);
		bool UpdateOwner(Owner owner);
		bool Save();
	}
}