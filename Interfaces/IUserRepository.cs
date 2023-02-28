using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IUserRepository
	{
		public bool CreateUser(User user);
		public bool Save();
		public bool UserExists(string email);
		public User GetUser(string email);
	}
}