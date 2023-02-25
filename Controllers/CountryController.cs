using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Repositories;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Dto;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountryController : Controller
	{
		private readonly ICountryRepository _countryRepository;
		public CountryController(ICountryRepository countryRepository)
		{
			_countryRepository = countryRepository;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(ICollection<CountryDto>))]
		[ProducesResponseType(400)]
		public IActionResult GetAllCountries()
		{
			var countries = _countryRepository.GetAllCountries();
			if(!ModelState.IsValid) return BadRequest();
			return Ok(countries);
		}

		[HttpGet("{countryId}")]
		[ProducesResponseType(200, Type = typeof(CountryDto))]
		[ProducesResponseType(404)]
		public IActionResult GetCountryById(int countryId)
		{
			if(!_countryRepository.CountryExists(countryId))
				return NotFound();

			var country = _countryRepository.GetCountry(countryId);
			if(!ModelState.IsValid)
				return BadRequest();
			return Ok(country);
		}

		[HttpGet("{countryId}/owners")]
		[ProducesResponseType(200, Type = typeof(ICollection<OwnerDto>))]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public IActionResult GetOwnersByCountry(int countryId)
		{
			if(!_countryRepository.CountryExists(countryId)) return NotFound();

			var owners = _countryRepository.GetOwnersByCountry(countryId);
			if(!ModelState.IsValid) 
				return BadRequest();
			return Ok(owners);
		}

		[HttpGet("owner/{ownerId}")]
		[ProducesResponseType(200, Type = typeof(OwnerDto))]
		[ProducesResponseType(404)]
		public IActionResult GetCountryByOwner(int ownerId)
		{
			var country = _countryRepository.GetCountryByOwner(ownerId);
			if(!ModelState.IsValid || country == null) return NotFound();
			return Ok(country);
		}
	}
}