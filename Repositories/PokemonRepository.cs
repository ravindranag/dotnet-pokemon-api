using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Postgres;

namespace PokemonReviewApp.Repositories
{
	public class PokemonRepository : IPokemonRepository
	{
		private readonly DataContext _context;
		public PokemonRepository(DataContext context)
		{
			_context = context;
		}
		public ICollection<Pokemon> GetPokemons() 
		{
			return _context.Pokemon.OrderBy(p => p.Id).ToList();
		}
		public Pokemon GetPokemon(int pokeId) => _context.Pokemon.Where(p => p.Id == pokeId).FirstOrDefault();

		public Pokemon GetPokemon(string name)
		{
			return _context.Pokemon.Where(p => p.Name == name).FirstOrDefault();
		}

		public double GetPokemonRating(int pokeId)
		{
			var reviews = _context.Reviews.Where(r => r.Pokemon.Id == pokeId);
			if(reviews.Count() <= 0) return 0;
			return (double)reviews.Sum(r => r.Rating) / reviews.Count();
		}

		public bool PokemonExists(int pokeId)
		{
			return _context.Pokemon.Any(p => p.Id == pokeId);
		}

		public bool PokemonExists(string name)
		{
			return _context.Pokemon.Any(p => p.Name.Trim().ToLower() == name.Trim().ToLower());
		}

		public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
		{
			var pokemonOwner = new PokemonOwner
			{
				OwnerId = ownerId,
				Pokemon = pokemon
			};

			var pokemonCategory = new PokemonCategory
			{
				CategoryId = categoryId,
				Pokemon = pokemon
			};

			_context.Add(pokemonOwner);
			_context.Add(pokemonCategory);
			_context.Add(pokemon);

			return Save();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}