using Mapster;
using Microsoft.AspNetCore.Http;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.Data;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using Silverhand.DAL.DTO.Updates;
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
     

        public TitlesService(
            ITitleRepository repository,
            IFileService fileService
          ): base(repository)// calling base class constructor
        {
            _repository = repository;
            _fileService = fileService;
          
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
             await _repository.AddAsync(entity);
          

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
            var titles = await _repository.GetAllAsync();

            var paged = titles
                .Skip((pageNum - 1) * pageSize)// skip previous pages
                .Take(pageSize)// take current page
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
        public async Task<TitleResponse> GetByIdTitle(Guid id, HttpRequest httpRequest)
        {
            var t = await _repository.GetByIdAsync(id);
            if (t == null)
                return null;

            return new TitleResponse
            {
                Id = t.Id,
                Name = t.Name,
                Type = t.Type,
                Description = t.Description,
                ReleaseYear = t.ReleaseYear,

                PosterUrl = t.PosterUrl != null
                    ? $"{httpRequest.Scheme}://{httpRequest.Host}/Images/{t.PosterUrl}"
                    : null
            };
        }

        public async Task<TitleResponse> UpdateAsync(Guid id, UpdateTitleRequest request)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("Title not found");

          
            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.ReleaseYear = request.ReleaseYear;
            entity.Type = request.Type;

            // handle image
            if (request.PosterImage != null)
            {
                if (!string.IsNullOrEmpty(entity.PosterUrl))
                {
                    _fileService.Delete(entity.PosterUrl);
                }

                var newImage = await _fileService.UploadAsync(request.PosterImage);
                entity.PosterUrl = newImage;
            }

          await  _repository.UpdateAsync(entity);
            

            return entity.Adapt<TitleResponse>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return false;

            // delete image from wwwroot
            if (!string.IsNullOrEmpty(entity.PosterUrl))
                _fileService.Delete(entity.PosterUrl);

            // delete from DB
            var result = await _repository.RemoveAsync(entity);

            return result > 0;
        }

    }
}
