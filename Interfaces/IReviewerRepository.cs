using PokemonReviewApp.Dto;

namespace PokemonReviewApp.Interfaces
{
	public interface IReviewerRepository
	{
		ICollection<ReviewerDto> GetAllReviewers();
		ReviewerDto GetReviewerById(int reviewerId);
		ICollection<ReviewDto> GetReviewsByReviewer(int reviewerId);
		bool ReviewerExists(int reviewerId);
	}
}