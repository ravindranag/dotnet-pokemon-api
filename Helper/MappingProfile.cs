using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Pokemon, PokemonDto>();
			CreateMap<Category, CategoryDto>();
			CreateMap<Owner, OwnerDto>();
			CreateMap<Country, CountryDto>();
			CreateMap<Review, ReviewDto>();
			CreateMap<Reviewer, ReviewerDto>();
			CreateMap<Reviewer, ReviewerWithReviewsDto>();
		}
	}
}