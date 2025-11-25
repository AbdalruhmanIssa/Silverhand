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
    public class IngestJobService : GenericService<IngestJobRequest, IngestJobResponse, IngestJob>, IIngestJobService
    {
      

        private readonly IIngestJobRepository _repository;
        private readonly IFileService _fileService;
        private readonly ApplicationDbContext _context;

        public IngestJobService(
            IIngestJobRepository repository,
            IFileService fileService,
            ApplicationDbContext context): base(repository)
        {
            _repository = repository;
            _fileService = fileService;
            _context = context;
        }

        public Task<IngestJobResponse> CreateAsync(IngestJobRequest request)
        {
            throw new NotImplementedException();
        }

        // --------------------------
        // CREATE (with Poster upload)
        // --------------------------
        public async Task<Guid> CreateFile(IngestJobRequest request)
        {
            var entity = request.Adapt<IngestJob>();
            entity.CreatedAt = DateTime.UtcNow;

            // Upload poster image
            if (request.SourceUrl != null)
            {
                var imagePath = await _fileService.UploadAsyncVid(request.SourceUrl);
                entity.SourceUrl = imagePath;
            }

            // Add entity
             await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

    

        // --------------------------
        // GET ALL WITH FULL URL + PAGINATION
        // --------------------------



    }
}
