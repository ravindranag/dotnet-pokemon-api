using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Dto;
using AutoMapper;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewerController : Controller
	{
		private readonly IReviewerRepository _reviewerRepository;
		private readonly IMapper _mapper;
		public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
		{	
			_reviewerRepository = reviewerRepository;
			_mapper = mapper;

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

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(422)]
		public IActionResult CreateReviewer([FromBody] ReviewerDto reviewer)
		{
			if(reviewer == null)
				return BadRequest();
			
			if(_reviewerRepository.ReviewerExists(reviewer.FirstName, reviewer.LastName))
			{
				ModelState.AddModelError("", "Reviewer already exists");
				return StatusCode(422, ModelState);
			}

			var newReviewer = _mapper.Map<Reviewer>(reviewer);
			if(!_reviewerRepository.CreateReviewer(newReviewer))
			{
				ModelState.AddModelError("", "Something went wrong");
				return StatusCode(500, ModelState);
			}

			return StatusCode(201, "Reviewer added successfully");
		}
	}
}