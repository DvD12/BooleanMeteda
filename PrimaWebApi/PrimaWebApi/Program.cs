
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PrimaWebApi.Code;
using PrimaWebApi.Data;
using PrimaWebApi.Loggers;
using PrimaWebApi.Middlewares;
using PrimaWebApi.Services;
using System.Text;

namespace PrimaWebApi
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
            builder.Services.AddSingleton<ICustomLogger, CustomConsoleLogger>();
            builder.Services.AddSingleton<PostRepository>();
            builder.Services.AddSingleton<CategoryRepository>();

			builder.Services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
              {
	              x.RequireHttpsMetadata = false;
	              x.SaveToken = true;
	              x.TokenValidationParameters = new TokenValidationParameters
	              {
		              ValidateIssuerSigningKey = true,
		              IssuerSigningKey = new SymmetricSecurityKey(
					            Encoding.ASCII.GetBytes(
					              builder.Configuration.GetSection("JwtSettings")
										               .Get<JwtSettings>().Key)),
		              ValidateIssuer = false,
		              ValidateAudience = false
	              };
              });
			builder.Services.AddScoped<IPasswordHasher<UserModel>, PasswordHasher<UserModel>>();
			builder.Services.AddScoped<JwtAuthenticationService>();
			builder.Services.AddScoped<UserService>();

			Console.WriteLine(builder.Configuration["MioDato"]); // Configuration mi permette di accedere alle proprietà di appsettings.json tramite una struttura tipo dizionario
            var app = builder.Build();

			app.UseMiddleware<LogMiddleware>();
			app.UseMiddleware<LogMiddleware2>();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
			app.UseAuthentication(); // Serve a JWT
			app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
