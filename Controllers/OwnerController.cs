using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Dto;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OwnerController : Controller
	{
		private readonly IOwnerRepository _ownerRepository;
		public OwnerController(IOwnerRepository ownerRepository)
		{
			_ownerRepository = ownerRepository;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(ICollection<OwnerDto>))]
		[ProducesResponseType(400)]
		public IActionResult GetAllOwners()
		{
			var owners = _ownerRepository.GetAllOwners();
			if(!ModelState.IsValid) return BadRequest();
			return Ok(owners);
		}

		[HttpGet("{ownerId}")]
		[ProducesResponseType(200, Type = typeof(OwnerDto))]
		[ProducesResponseType(404)]
		public IActionResult GetOwnerById(int ownerId)
		{
			var owner = _ownerRepository.GetOwnerById(ownerId);
			if(!ModelState.IsValid || owner == null) return NotFound();
			return Ok(owner);
		}

		[HttpGet("{ownerId}/pokemons")]
		[ProducesResponseType(200, Type = typeof(ICollection<PokemonDto>))]
		[ProducesResponseType(404)]
		public IActionResult GetPokemonByOwner(int ownerId)
		{
			var pokemons = _ownerRepository.GetPokemonByOwner(ownerId);
			if(!ModelState.IsValid) return BadRequest();
			return Ok(pokemons);
		}

		[HttpGet("pokemon/{pokeId}")]
		[ProducesResponseType(200, Type = typeof(OwnerDto))]
		[ProducesResponseType(404)]
		public IActionResult GetOwnerByPokemon(int pokeId)
		{
			var owner = _ownerRepository.GetOwnerByPokemon(pokeId);
			if(!ModelState.IsValid || owner == null) return NotFound();
			return Ok(owner);
		}
	}
}