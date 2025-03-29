
using InternIntellegence_Portfolio.DbHelper.DatabaseContext;
using InternIntellegence_Portfolio.DbHelper.Repos;
using InternIntellegence_Portfolio.Models;
using InternIntellegence_Portfolio.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace InternIntellegence_Portfolio
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => 
			{
				// Configure Identity options
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequiredLength = 8;

				// User settings
				options.User.RequireUniqueEmail = true;
			})
			.AddEntityFrameworkStores<ApplicationContext>() 
			.AddDefaultTokenProviders();

			// Add Authentication
			builder.Services.AddAuthentication();

			builder.Services.AddScoped<ValidationRepo>();
			builder.Services.AddScoped<AccountManagementService>();
			builder.Services.AddScoped<ProtfolioManagementService>();
			builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>)); // register all repos

			builder.Services.AddDbContext<ApplicationContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("HostingConnection")));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
