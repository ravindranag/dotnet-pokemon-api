using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Postgres;

namespace PokemonReviewApp.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly DataContext _context;
		public CategoryRepository(DataContext context)
		{
			_context = context;
		}

		public bool CategoryExists(int catId)
		{
			return _context.Categories.Any(c => c.Id == catId);
		}

		public ICollection<Category> GetCategories()
		{
			return _context.Categories.OrderBy(c => c.Id).ToList();
		}

		public Category GetCategoryById(int catId)
		{
			return _context.Categories.Where(c => c.Id == catId).FirstOrDefault();
		}

		ICollection<Pokemon> ICategoryRepository.GetPokemonsByCategory(int catId)
		{
			return _context.PokemonCategories.Where(pc => pc.CategoryId == catId).Select(pc => pc.Pokemon).ToList();
		}
	}
}