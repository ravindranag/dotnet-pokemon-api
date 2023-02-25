namespace PokemonReviewApp.Dto {
	public class ReviewDto {
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public decimal Rating { get; set; }
	}

	public class CreateReviewDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public decimal Rating { get; set; }
		public int ReviewerId { get; set; }
		public int PokemonId { get; set; }
	}
}