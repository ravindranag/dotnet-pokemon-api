using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IReviewRepository
	{
		ICollection<ReviewDto> GetAllReviews();
		ICollection<ReviewDto> GetReviewByPokemon(int pokeId);
		ReviewDto GetReviewById(int reviewId);
		bool ReviewExists(int reviewId);
		bool CreateReview(Review review);
		bool Save();
	}
}