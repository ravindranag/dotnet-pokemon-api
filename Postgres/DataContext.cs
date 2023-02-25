using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Postgres {
	public class DataContext:  DbContext {

		public DataContext(DbContextOptions<DataContext> options): base(options) {

		}
		public DbSet<Pokemon> Pokemon { get; set; }
		public DbSet<Owner> Owners { get; set; }
		public DbSet<Category> Categories { set; get; }
		public DbSet<Country> Countries { set; get; }
		public DbSet<Review> Reviews { set; get; }
		public DbSet<Reviewer> Reviewers { set; get; }
		public DbSet<PokemonCategory> PokemonCategories { set; get; }
		public DbSet<PokemonOwner> PokemonOwners { set; get; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<PokemonOwner>()
				.HasKey(po => new { po.OwnerId, po.PokemonId });
			modelBuilder.Entity<PokemonOwner>()
				.HasOne(p => p.Pokemon)
				.WithMany(po => po.PokemonOwners)
				.HasForeignKey(p => p.PokemonId);
			modelBuilder.Entity<PokemonOwner>()
				.HasOne(o => o.Owner)
				.WithMany(po => po.PokemonOwners)
				.HasForeignKey(o => o.OwnerId);


			modelBuilder.Entity<PokemonCategory>()
				.HasKey(pc => new { pc.CategoryId, pc.PokemonId });
			modelBuilder.Entity<PokemonCategory>()
				.HasOne(p => p.Pokemon)
				.WithMany(pc => pc.PokemonCategories)
				.HasForeignKey(p => p.PokemonId);
			modelBuilder.Entity<PokemonCategory>()
				.HasOne(c => c.Category)
				.WithMany(pc => pc.PokemonCategories)
				.HasForeignKey(c => c.CategoryId);

			modelBuilder.Entity<Owner>()
				.HasOne<Country>(c => c.Country)
				.WithMany(c => c.Owners)
				.HasForeignKey(o => o.CountryId);

			modelBuilder.Entity<Review>()
				.HasOne<Reviewer>(r => r.Reviewer)
				.WithMany(r => r.Reviews)
				.HasForeignKey(r => r.ReviewerId);
		}

		// protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql();
		
	}
}