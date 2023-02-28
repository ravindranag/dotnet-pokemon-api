using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Postgres;
using AutoMapper;

namespace PokemonReviewApp.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public UserRepository(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public bool CreateUser(User user)
		{
			_context.Add(user);
			return Save();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UserExists(string email)
		{
			return _context.Users.Any(u => u.Email == email);
		}

		public User GetUser(string email)
		{
			return _context.Users.Where(u => u.Email == email).FirstOrDefault();
		}
	}
}