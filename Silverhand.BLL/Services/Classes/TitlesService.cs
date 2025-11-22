using Mapster;
using Microsoft.AspNetCore.Http;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.Data;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using Silverhand.DAL.Models;
using Silverhand.DAL.Repository.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Classes
{
    public class TitlesService : GenericService<TitleRequest, TitleResponse, Title>, ITitlesService
    {
        private readonly ITitleRepository _repository;
        private readonly IFileService _fileService;
        private readonly ApplicationDbContext _context;

        public TitlesService(
            ITitleRepository repository,
            IFileService fileService,
            ApplicationDbContext context): base(repository)
        {
            _repository = repository;
            _fileService = fileService;
            _context = context;
        }

        // --------------------------
        // CREATE (with Poster upload)
        // --------------------------
        public async Task<Guid> CreateFile(TitleRequest request)
        {
            var entity = request.Adapt<Title>();
            entity.CreatedAt = DateTime.UtcNow;

            // Upload poster image
            if (request.PosterUrl != null)
            {
                var imagePath = await _fileService.UploadAsync(request.PosterUrl);
                entity.PosterUrl = imagePath;
            }

            // Add entity
             _repository.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        // --------------------------
        // GET ALL WITH FULL URL + PAGINATION
        // --------------------------
        public async Task<List<TitleResponse>> GetTitles(
            HttpRequest httpRequest, bool onlyActive = false,
            int pageNum = 1,
            int pageSize = 10)
        {
            var titles =  _repository.GetAll();

            var paged = titles
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return paged.Select(t => new TitleResponse
            {
                Id = t.Id,
                Name = t.Name,
                Type = t.Type,
                Description = t.Description,
                ReleaseYear = t.ReleaseYear,

                PosterUrl = t.PosterUrl != null
                    ? $"{httpRequest.Scheme}://{httpRequest.Host}/Images/{t.PosterUrl}"
                    : null

            }).ToList();
        }

      
    }
}
