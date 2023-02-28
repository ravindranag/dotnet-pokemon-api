namespace PokemonReviewApp.Dto 
{
	public class PokemonDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
	}

	public class PokemonUpdateDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
		public int OwnerId { get; set; }
		public int CategoryId { get; set; }
	}
}