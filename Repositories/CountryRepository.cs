using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Postgres;
using PokemonReviewApp.Dto;
using AutoMapper;

namespace PokemonReviewApp.Repositories
{
	public class CountryRepository : ICountryRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;
		public CountryRepository(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public bool CountryExists(int countryId)
		{
			return _context.Countries.Any(c => c.Id == countryId);
		}

		public ICollection<CountryDto> GetAllCountries()
		{
			return _mapper.Map<ICollection<CountryDto>>(_context.Countries.OrderBy(c => c.Id).ToList());
		}

		public CountryDto GetCountry(int countryId)
		{
			return _mapper.Map<CountryDto>(_context.Countries.Where(c => c.Id == countryId).FirstOrDefault());
		}

		public CountryDto GetCountryByOwner(int ownerId)
		{
			return _mapper.Map<CountryDto>(_context.Owners.Where(o => o.Id == ownerId).Select(o => o.Country).FirstOrDefault());
		}

		public ICollection<OwnerDto> GetOwnersByCountry(int countryId)
		{
			return _mapper.Map<ICollection<OwnerDto>>(_context.Countries.Where(c => c.Id == countryId).SelectMany(c => c.Owners).ToList());
		}
	}
}