using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class PokemonController : Controller
	{
		private readonly IPokemonRepository _pokemonRepository;
		private readonly IMapper _mapper;
		public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper)
		{
			_pokemonRepository = pokemonRepository;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(ICollection<Pokemon>))]
		public IActionResult GetAllPokemon()
		{
			var result = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());
			if(!ModelState.IsValid) {
				return BadRequest(ModelState);
			}
			
			return Ok(result);
		}

		[HttpGet("{pokeId}")]
		[ProducesResponseType(200, Type = typeof(PokemonDto))]
		[ProducesResponseType(404)]
		[ProducesResponseType(400)]
		public IActionResult GetPokemonById(int pokeId)
		{
			if(!_pokemonRepository.PokemonExists(pokeId)) return NotFound();
			var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(pokeId));
			if(!ModelState.IsValid) return BadRequest(ModelState);
			return Ok(pokemon);
		}


		[HttpGet("{pokeId}/rating")]
		[ProducesResponseType(200, Type = typeof(decimal))]
		[ProducesResponseType(404)]
		[ProducesResponseType(400)]
		public IActionResult GetPokemonRating(int pokeId)
		{
			if(!_pokemonRepository.PokemonExists(pokeId)) return NotFound();
			var rating = _pokemonRepository.GetPokemonRating(pokeId);
			if(!ModelState.IsValid) return BadRequest(ModelState);
			return Ok(rating);
		}
	}
}