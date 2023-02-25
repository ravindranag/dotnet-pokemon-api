using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Postgres;
using AutoMapper;

namespace PokemonReviewApp.Repositories
{

	public class OwnerRepository : IOwnerRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public OwnerRepository(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public ICollection<OwnerDto> GetAllOwners()
		{
			return _mapper.Map<ICollection<OwnerDto>>(_context.Owners.OrderBy(o => o.Id).ToList());
		}

		public OwnerDto GetOwnerById(int ownerId)
		{
			return _mapper.Map<OwnerDto>(_context.Owners.Where(o => o.Id == ownerId).FirstOrDefault());
		}

		public OwnerDto GetOwnerByPokemon(int pokeId)
		{
			return _mapper.Map<OwnerDto>(_context.PokemonOwners.Where(po => po.PokemonId == pokeId).Select(po => po.Owner).FirstOrDefault());
		}

		public ICollection<PokemonDto> GetPokemonByOwner(int ownerId)
		{
			return _mapper.Map<ICollection<PokemonDto>>(_context.PokemonOwners.Where(po => po.OwnerId == ownerId).Select(po => po.Pokemon));
		}

		public bool OwnerExists(int ownerId)
		{
			return _context.Owners.Any(o => o.Id == ownerId);
		}
	}
}
