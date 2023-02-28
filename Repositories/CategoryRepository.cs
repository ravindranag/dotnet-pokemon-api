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

		public ICollection<Pokemon> GetPokemonsByCategory(int catId)
		{
			return _context.PokemonCategories.Where(pc => pc.CategoryId == catId).Select(pc => pc.Pokemon).ToList();
		}

		public bool CreateCategory(Category category)
		{
			_context.Add(category);
			return Save();
		}

		public bool UpdateCategory(Category category)
		{
			_context.Update(category);
			return Save();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}