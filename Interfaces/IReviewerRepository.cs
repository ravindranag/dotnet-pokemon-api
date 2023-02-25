using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IReviewerRepository
	{
		ICollection<ReviewerDto> GetAllReviewers();
		ReviewerWithReviewsDto GetReviewerById(int reviewerId);
		ICollection<ReviewDto> GetReviewsByReviewer(int reviewerId);
		bool ReviewerExists(int reviewerId);
	}
}