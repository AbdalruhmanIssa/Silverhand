
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Silverhand.BLL.Services.Classes;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.Data;
using Silverhand.DAL.Models;
using Silverhand.DAL.Repository.Classes;
using Silverhand.DAL.Repository.Repositories;
using Silverhand.DAL.Utilites;
using Silverhand.PL.Uti;
using System.Text;

namespace Silverhand.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
            // Add services to the container.
            builder.Services.AddScoped<ITitleRepository, TitleRepository>();
            builder.Services.AddScoped<ITitlesService, TitlesService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();
            builder.Services.AddScoped<IEpisodeService, EpisodeService>();
            builder.Services.AddScoped<IAssetService, AssetService>();
            builder.Services.AddScoped<IAssetRepository, AssetRepository>();
            builder.Services.AddScoped<ISubtitleService,SubtitleService>();
            builder.Services.AddScoped<ISubtitleRepository, SubtitleRepository>();
            builder.Services.AddScoped<IThumbmailService, ThumbnailService>();
            builder.Services.AddScoped<IThumbnailRepository, ThumbnailRepository>();
            builder.Services.AddScoped<IIngestJobService, IngestJobService>();
            builder.Services.AddScoped<IIngestJobRepository, IngestJobRepository>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISeedData, SeedData>();
            builder.Services.AddScoped<IEmailSender, Emailsetting>();
            builder.Services.AddScoped<IProfileService, ProfileService>();
            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
            builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            builder.Services.AddScoped<IFavoriteService, FavoriteService>();
            builder.Services.AddScoped<IWatchProgressRepository, WatchProgressRepository>();
            builder.Services.AddScoped<IWatchProgressService, WatchProgressService>();
            builder.Services.AddScoped<IPlanService, PlanService>();
            builder.Services.AddScoped<IPlanRepository, PlanRepository>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;// X cockies T JWT
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; //returns 401 unauth
            })

                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = false,
                           ValidateAudience = false,
                           ValidateLifetime = true,//duration of token
                           ValidateIssuerSigningKey = true,//signiture
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("jwtOptions")["SecretKey"]))//encrypyion
                       ,
                           RoleClaimType = "Role"
                       };
                   });


            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            var scope = app.Services.CreateScope();
            var seed = scope.ServiceProvider.GetRequiredService<ISeedData>();
            await seed.DataSeeding();
            await seed.IdentityDataSeedingAsync();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseCors("AllowAll");
            app.UseAuthorization();

            app.UseAuthorization();
            app.UseStaticFiles();


            app.MapControllers();

            app.Run();
        }
    }
}
