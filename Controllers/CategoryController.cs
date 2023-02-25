using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using AutoMapper;
using PokemonReviewApp.Models;
using PokemonReviewApp.Dto;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : Controller
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;
		public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
		{
			_categoryRepository = categoryRepository;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IList<CategoryDto>))]
		[ProducesResponseType(400)]
		public IActionResult GetAllCategories()
		{
			var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
			if(!ModelState.IsValid) return BadRequest();
			return Ok(categories);
		}

		[HttpGet("{catId}")]
		[ProducesResponseType(200, Type = typeof(CategoryDto))]
		[ProducesResponseType(404)]
		public IActionResult GetCategoryById(int catId)
		{
			if(!_categoryRepository.CategoryExists(catId)) return NotFound("Category not found");
			var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategoryById(catId));
			if(!ModelState.IsValid) return BadRequest();
			return Ok(category);
		}

		[HttpGet("{catId}/pokemon")]
		[ProducesResponseType(200, Type = typeof(ICollection<PokemonDto>))]
		[ProducesResponseType(400)]
		public IActionResult GetPokemonsByCategory(int catId)
		{
			if(!_categoryRepository.CategoryExists(catId)) return NotFound("Category not found");
			var pokemons = _mapper.Map<List<PokemonDto>>(_categoryRepository.GetPokemonsByCategory(catId));
			if(!ModelState.IsValid) return BadRequest();

			return Ok(pokemons);
		}

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(422)]
		public IActionResult CreateCategory([FromBody] CategoryDto category)
		{
			if(category == null)
				return BadRequest(ModelState);

			var existingCategory = _categoryRepository.GetCategories()
				.Where(c => c.Name.Trim().ToUpper() == category.Name.Trim().ToUpper())
				.FirstOrDefault();

			if(existingCategory != null)
			{
				ModelState.AddModelError("UniqueConstraintFailed", "Category already exists");
				return StatusCode(422, ModelState);
			}			

			var newCategory = _mapper.Map<Category>(category);
			if(!_categoryRepository.CreateCategory(newCategory))
			{
				ModelState.AddModelError("ServerFailed", "Something went wrong!");
				return StatusCode(500, ModelState);
			}
			return StatusCode(201, "Category created successfully");
		}
	}
}