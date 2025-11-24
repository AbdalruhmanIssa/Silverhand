
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Silverhand.BLL.Services.Classes;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.Data;
using Silverhand.DAL.Repository.Classes;
using Silverhand.DAL.Repository.Repositories;

namespace Silverhand.PL
{
    public class Program
    {
        public static void Main(string[] args)
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
