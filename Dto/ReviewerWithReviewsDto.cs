namespace PokemonReviewApp.Dto {
	public class ReviewerWithReviewsDto {
		public int Id { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public ICollection<ReviewDto>? Reviews { get; set; }
	}
}