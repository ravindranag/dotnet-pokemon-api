using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;
using AutoMapper;
using PokemonReviewApp.Helper;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : Controller
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;

		public UserController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_configuration = configuration;
		}
		
		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
			passwordSalt = hmac.Key;
			passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
				return computedHash.SequenceEqual(passwordHash);
			}
		}

		private string CreateToken(User user) 
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, user.Email)
			};
			var key = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(
					_configuration.GetSection("JWT:Secret").Value!
				)
			);
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(30),
				signingCredentials: creds
			);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}

		[HttpPost]
		[ProducesResponseType(201, Type = typeof(string))]
		[ProducesResponseType(400)]
		public IActionResult CreateUser([FromBody] UserDto user)
		{
			if(user == null)
				return BadRequest();

			if(_userRepository.UserExists(user.Email))
			{
				ModelState.AddModelError("User", "User with email already exists.");
				return StatusCode(400, ModelState);
			}

			CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);			
			var newUser = _mapper.Map<User>(user);
			newUser.PasswordHash = passwordHash;
			newUser.PasswordSalt = passwordSalt;

			if(!_userRepository.CreateUser(newUser))
			{
				ModelState.AddModelError("User", "Something went wrong creating user");
				return StatusCode(500, ModelState);
			}

			return Ok("User created");
		}

		[HttpPost("login")]
		[ProducesResponseType(200, Type = typeof(string))]
		[ProducesResponseType(401)]
		public IActionResult UserLogin([FromBody] UserDto user)
		{
			if(user == null)
				return BadRequest();

			if(!_userRepository.UserExists(user.Email))
			{
				ModelState.AddModelError("User", "User not registered. Please login to continue");
				return StatusCode(400, ModelState);
			}

			var requestedUser = _userRepository.GetUser(user.Email);
			if(!VerifyPasswordHash(user.Password, requestedUser.PasswordHash, requestedUser.PasswordSalt))
			{
				ModelState.AddModelError("User", "Invalid password");
				return StatusCode(401, ModelState);
			}

			var jwt = CreateToken(requestedUser);

			return Ok(jwt);
		}
	}
}