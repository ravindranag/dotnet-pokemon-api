using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface ICategoryRepository
	{
		ICollection<Category> GetCategories();
		Category GetCategoryById(int catId);
		bool CategoryExists(int catId);
		ICollection<Pokemon> GetPokemonsByCategory(int catId);
		bool CreateCategory(Category category);
		bool Save();
		bool UpdateCategory(Category category);
	}
}