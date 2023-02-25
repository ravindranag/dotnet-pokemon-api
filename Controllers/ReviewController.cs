using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Dto;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewController : Controller
	{
		private readonly IReviewRepository _reviewRepository;
		public ReviewController(IReviewRepository reviewRepository)
		{
			_reviewRepository = reviewRepository;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(ICollection<ReviewDto>))]
		[ProducesResponseType(400)]
		public IActionResult GetAllReviews()
		{
			var reviews = _reviewRepository.GetAllReviews();
			if(!ModelState.IsValid) return BadRequest();
			return Ok(reviews);
		}

		[HttpGet("{reviewId}")]
		[ProducesResponseType(200, Type = typeof(ICollection<ReviewDto>))]
		[ProducesResponseType(404)]
		public IActionResult GetReviewById(int reviewId)
		{
			var review = _reviewRepository.GetReviewById(reviewId);
			if(!ModelState.IsValid || review == null) return NotFound();
			return Ok(review);
		}

		[HttpGet("pokemon/{pokeId}")]
		[ProducesResponseType(200, Type = typeof(ICollection<ReviewDto>))]
		[ProducesResponseType(404)]
		public IActionResult GetReviewByPokemon(int pokeId)
		{
			var reviews = _reviewRepository.GetReviewByPokemon(pokeId);
			if(!ModelState.IsValid) return BadRequest();
			return Ok(reviews);
		}
	}
}