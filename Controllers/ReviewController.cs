using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Dto;
using AutoMapper;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewController : Controller
	{
		private readonly IReviewRepository _reviewRepository;
		private readonly IMapper _mapper;
		public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
		{
			_reviewRepository = reviewRepository;
			_mapper = mapper;
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

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(422)]
		public IActionResult CreateReview([FromBody] CreateReviewDto review)
		{
			if(review == null)
				return BadRequest();
		
			var newReview = _mapper.Map<Review>(review);
			if(!_reviewRepository.CreateReview(newReview))
			{
				ModelState.AddModelError("", "Something went wrong");
				return StatusCode(500, ModelState);
			}
			return StatusCode(201, "Review added");
		}
	}
}