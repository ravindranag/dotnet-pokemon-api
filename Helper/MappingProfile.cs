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
			CreateMap<PokemonDto, Pokemon>();
			CreateMap<Category, CategoryDto>();
			CreateMap<CategoryDto, Category>();
			CreateMap<Owner, OwnerDto>();
			CreateMap<OwnerDto, Owner>();
			CreateMap<OwnerCreateDto, Owner>();
			CreateMap<Country, CountryDto>();
			CreateMap<CountryDto, Country>();
			CreateMap<Review, ReviewDto>();
			CreateMap<CreateReviewDto, Review>();
			CreateMap<Reviewer, ReviewerDto>();
			CreateMap<ReviewerDto, Reviewer>();
			CreateMap<Reviewer, ReviewerWithReviewsDto>();
		}
	}
}