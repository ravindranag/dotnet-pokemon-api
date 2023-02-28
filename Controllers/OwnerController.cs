using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;
using AutoMapper;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OwnerController : Controller
	{
		private readonly IOwnerRepository _ownerRepository;
		private readonly IMapper _mapper;
		private readonly ICountryRepository _countryRepository;
		public OwnerController(IOwnerRepository ownerRepository, ICountryRepository countryRepository, IMapper mapper)
		{
			_ownerRepository = ownerRepository;
			_mapper = mapper;
			_countryRepository = countryRepository;
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

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(422)]
		public IActionResult CreateOwner([FromBody] OwnerCreateDto owner)
		{
			if(owner == null)
				return BadRequest();

			if(_ownerRepository.OwnerExists(owner.FirstName, owner.LastName))
			{
				ModelState.AddModelError("", "Owner already exists");
				return StatusCode(422, ModelState);
			}
			var newOwner = _mapper.Map<Owner>(owner);
			if(!_ownerRepository.CreateOwner(newOwner))
			{
				ModelState.AddModelError("", "Something went wrong");
				StatusCode(500, ModelState);
			}
			return StatusCode(201, "Owner added successfully");
		}

		[HttpPut("{ownerId}")]
		[ProducesResponseType(200, Type = typeof(string))]
		[ProducesResponseType(404)]
		[ProducesResponseType(400)]
		public IActionResult UpdateOwner(int ownerId, [FromBody] OwnerCreateDto owner)
		{
			if(owner == null)
				return BadRequest();

			if(!_ownerRepository.OwnerExists(ownerId))
			{
				ModelState.AddModelError("", "Owner not found");
				return StatusCode(404, ModelState);
			}

			var newOwner = _mapper.Map<Owner>(owner);
			newOwner.Id = ownerId;
			if(!_ownerRepository.UpdateOwner(newOwner))
			{
				ModelState.AddModelError("", "Something went wrong");
				return StatusCode(500, ModelState);
			}

			return Ok("Owner updated");
		}
	}
}