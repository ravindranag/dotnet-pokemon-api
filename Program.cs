using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Postgres;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Repositories;
using PokemonReviewApp;
using System.Text.Json.Serialization;
using PokemonReviewApp.Helper;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddControllers();
		builder.Services.AddTransient<Seed>();
		builder.Services.AddControllers().AddJsonOptions(x =>
			x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
		builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
		builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
		builder.Services.AddScoped<ICountryRepository, CountryRepository>();
		builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
		builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
		builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();
		builder.Services.AddScoped<IUserRepository, UserRepository>();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.AddAuthentication().AddJwtBearer(options => {
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				ValidateAudience = true,
				ValidateIssuer = true,
				IssuerSigningKey = new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(
						builder.Configuration["JWT:Secret"]!
					)
				),
				ValidIssuer = builder.Configuration["JWT:Issuer"],
				ValidAudience = builder.Configuration["JWT:Audience"]
			};
		});
		builder.Services.AddAuthorization();

		builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration["ConnectionStrings:Database"]));

		var app = builder.Build();

		if (args.Length == 1 && args[0].ToLower() == "seeddata")
			SeedData(app);

		void SeedData(IHost app)
		{
			var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

			using (var scope = scopedFactory.CreateScope())
			{
				var service = scope.ServiceProvider.GetService<Seed>();
				service.SeedDataContext();
			}
		}

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseAuthentication();
		app.UseAuthorization();
		app.UseCors(builder =>
		{
			builder
				.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod();
		});

		app.MapControllers();

		app.Run();
	}
}