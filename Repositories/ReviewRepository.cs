using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Postgres;
using AutoMapper;

namespace PokemonReviewApp.Repositories
{
	public class ReviewRepository : IReviewRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;
		public ReviewRepository(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		} 
		public ICollection<ReviewDto> GetAllReviews()
		{
			return _mapper.Map<ICollection<ReviewDto>>(_context.Reviews.OrderBy(r => r.Id).ToList());
		}

		public ReviewDto GetReviewById(int reviewId)
		{
			return _mapper.Map<ReviewDto>(_context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault());
		}

		public ICollection<ReviewDto> GetReviewByPokemon(int pokeId)
		{
			return _mapper.Map<ICollection<ReviewDto>>(_context.Reviews.Where(r => r.Pokemon.Id == pokeId).ToList());
		}

		public bool ReviewExists(int reviewId)
		{
			return _context.Reviews.Any(r => r.Id == reviewId);
		}
	}
}