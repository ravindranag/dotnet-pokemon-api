using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Postgres;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
	public class ReviewerRepository : IReviewerRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;
		public ReviewerRepository(DataContext context, IMapper mapper)
		{	
			_context = context;
			_mapper = mapper;
		}
		public ICollection<ReviewerDto> GetAllReviewers()
		{	
			return _mapper.Map<ICollection<ReviewerDto>>(_context.Reviewers.OrderBy(r => r.Id).ToList());
		}

		public ReviewerWithReviewsDto GetReviewerById(int reviewerId)
		{
			return _mapper.Map<ReviewerWithReviewsDto>(_context.Reviewers.Where(r => r.Id == reviewerId).Include(r => r.Reviews).FirstOrDefault());
		}

		public ICollection<ReviewDto> GetReviewsByReviewer(int reviewerId)
		{
			return _mapper.Map<ICollection<ReviewDto>>(_context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList());
		}

		public bool ReviewerExists(int reviewerId)
		{
			return _context.Reviewers.Any(r => r.Id == reviewerId);
		}
	}
}