using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Dto;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewerController : Controller
	{
		private readonly IReviewerRepository _reviewerRepository;
		public ReviewerController(IReviewerRepository reviewerRepository)
		{	
			_reviewerRepository = reviewerRepository;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(ICollection<ReviewerDto>))]
		[ProducesResponseType(400)]
		public IActionResult GetAllReviewers()
		{
			var reviewers = _reviewerRepository.GetAllReviewers();
			if(!ModelState.IsValid) return BadRequest(ModelState);
			return Ok(reviewers);
		}

		[HttpGet("{reviewerId}")]
		[ProducesResponseType(200, Type = typeof(ReviewerWithReviewsDto))]
		[ProducesResponseType(400)]
		public IActionResult GetReviewerById(int reviewerId)
		{
			var reviewer = _reviewerRepository.GetReviewerById(reviewerId);
			if(!ModelState.IsValid || reviewer == null) return NotFound();
			return Ok(reviewer);
		}

		[HttpGet("{reviewerId}/reviews")]
		[ProducesResponseType(200, Type = typeof(ICollection<ReviewDto>))]
		[ProducesResponseType(400)]
		public IActionResult GetReviewsByReviewer(int reviewerId)
		{
			if(!_reviewerRepository.ReviewerExists(reviewerId)) return NotFound();
			var reviews = _reviewerRepository.GetReviewsByReviewer(reviewerId);
			if(!ModelState.IsValid) return BadRequest(ModelState);

			return Ok(reviews);
		}
	}
}