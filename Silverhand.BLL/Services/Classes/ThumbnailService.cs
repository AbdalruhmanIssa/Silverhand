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
    public class ThumbnailService : GenericService<ThumbnailRequest, ThumbnailResponse, Thumbnail>, IThumbmailService
    {
        private readonly IThumbnailRepository _repository;
        private readonly IFileService _fileService;
        private readonly ApplicationDbContext _context;

        public ThumbnailService(
            IThumbnailRepository repository,
            IFileService fileService,
            ApplicationDbContext context
        ) : base(repository)
        {
            _repository = repository;
            _fileService = fileService;
            _context = context;
        }

        // SPECIAL CREATE WITH FILE UPLOAD + TITLE LOGIC
        public async Task<Guid> CreateFile(ThumbnailRequest request)
        {
            // Validate episode
            var episode = await _context.Episodes.FindAsync(request.EpisodeId);
            if (episode == null)
                throw new Exception("Episode not found");

            // Map request → entity
            var entity = request.Adapt<Thumbnail>();
            entity.CreatedAt = DateTime.UtcNow;

            // Set TitleId from Episode
            entity.TitleId = episode.TitleId;

            // Upload image if exists
            if (request.ImageUrl != null)
            {
                var path = await _fileService.UploadAsync(request.ImageUrl);
                entity.ImageUrl = path;
            }

            await _repository.AddAsync(entity);
           

            return entity.Id;
        }
        public async Task<ThumbnailResponse> GetByIdThumbnail(Guid id, HttpRequest httpRequest)
        {
            var t = await _repository.GetByIdAsync(id);
            if (t == null)
                return null;

            return new ThumbnailResponse
            {
                Id = t.Id,
                EpisodeId = t.EpisodeId,
                

                SceneSecond = t.SceneSecond,

                ImageUrl = t.ImageUrl != null
                    ? $"{httpRequest.Scheme}://{httpRequest.Host}/Images/{t.ImageUrl}"
                    : null
            };
        }

        public async Task<ThumbnailResponse> UpdateAsync(Guid id, UpdateThumbnailRequest request)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("Thumbnail not found");

            // Update fields
            entity.SceneSecond = request.SceneSecond ?? entity.SceneSecond;

            // If you want EpisodeId to be updatable (your choice):
            if (request.EpisodeId != Guid.Empty && request.EpisodeId != entity.EpisodeId)
            {
                var episode = await _context.Episodes.FindAsync(request.EpisodeId);
                if (episode == null)
                    throw new Exception("Episode not found");

                entity.EpisodeId = episode.Id;
                entity.TitleId = episode.TitleId; // maintain consistency
            }

            // Handle image replacement
            if (request.ImageUrl != null)
            {
                // Delete old image
                if (!string.IsNullOrWhiteSpace(entity.ImageUrl))
                    _fileService.Delete(entity.ImageUrl);

                // Upload new file
                var newImg = await _fileService.UploadAsync(request.ImageUrl);
                entity.ImageUrl = newImg;
            }

            await _repository.UpdateAsync(entity);

            return entity.Adapt<ThumbnailResponse>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                return false;

            // delete image from disk
            if (!string.IsNullOrEmpty(entity.ImageUrl))
                _fileService.Delete(entity.ImageUrl);

            var result = await _repository.RemoveAsync(entity);

            return result > 0;
        }

    }

}
