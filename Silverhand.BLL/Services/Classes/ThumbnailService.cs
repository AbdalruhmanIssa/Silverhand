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
            await _context.SaveChangesAsync();

            return entity.Id;
        }
    }

}
