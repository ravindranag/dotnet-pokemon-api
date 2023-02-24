using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PokemonController : Controller
	{
		private readonly IPokemonRepository _pokemonRepository;
		public PokemonController(IPokemonRepository pokemonRepository)
		{
			_pokemonRepository = pokemonRepository;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(ICollection<Pokemon>))]
		public IActionResult GetAllPokemon()
		{
			var result = _pokemonRepository.GetPokemons();
			if(!ModelState.IsValid) {
				return BadRequest(ModelState);
			}
			
			return Ok(result);
		}
	}
}