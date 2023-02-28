using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Models;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Dto;
using AutoMapper;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountryController : Controller
	{
		private readonly ICountryRepository _countryRepository;
		private readonly IMapper _mapper;
		public CountryController(ICountryRepository countryRepository, IMapper mapper)
		{
			_countryRepository = countryRepository;
			_mapper = mapper;
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

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(422)]
		public IActionResult CreateCountry([FromBody] CountryDto country)
		{
			if(country == null)
				return BadRequest(ModelState);

			if(_countryRepository.CountryExists(country.Name))
			{
				ModelState.AddModelError("", "Country already exists");
				return StatusCode(422, ModelState);
			}

			var newCountry = _mapper.Map<Country>(country);
			if(_countryRepository.CreateCountry(newCountry))
			{
				ModelState.AddModelError("", "Something went wrong");
				StatusCode(500, ModelState);
			}
			return StatusCode(201, "Country added successfully");
		}

		[HttpPut("{countryId}")]
		[ProducesResponseType(200, Type = typeof(string))]
		[ProducesResponseType(404)]
		[ProducesResponseType(400)]
		public IActionResult UpdateCountry(int countryId, [FromBody] CountryDto country)
		{
			if(country == null)
				return BadRequest();
			
			if(!_countryRepository.CountryExists(countryId))
			{
				ModelState.AddModelError("", "Country does not exist");
				return StatusCode(404, ModelState);
			}

			var newCountry = _mapper.Map<Country>(country);
			newCountry.Id = countryId;

			if(!_countryRepository.UpdateCountry(newCountry))
			{
				ModelState.AddModelError("", "Something went wrong");
				return StatusCode(500, ModelState);
			}

			return Ok("Country updated");
		}
	}
}