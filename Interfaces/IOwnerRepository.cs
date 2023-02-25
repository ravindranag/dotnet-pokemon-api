using PokemonReviewApp.Dto;

namespace PokemonReviewApp.Interfaces
{
	public interface IOwnerRepository
	{
		ICollection<OwnerDto> GetAllOwners();
		OwnerDto GetOwnerById(int ownerId);
		ICollection<PokemonDto> GetPokemonByOwner(int ownerId);
		OwnerDto GetOwnerByPokemon(int pokeId);
		bool OwnerExists(int ownerId);
	}
}