using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IPokemonRepository
	{
		ICollection<Pokemon> GetPokemons();
		Pokemon GetPokemon(int pokeId);
		Pokemon GetPokemon(string name);
		double GetPokemonRating(int pokeId);
		bool PokemonExists(int pokeId);
	}
}