using Mapster;
using Microsoft.AspNetCore.Http;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.Data;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using Silverhand.DAL.DTO.Updates;
using Silverhand.DAL.Models;
using Silverhand.DAL.Repository.Classes;
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
        private readonly IAssetRepository _assetRepo;

        public IngestJobService(
            IIngestJobRepository repository,
            IFileService fileService,
            ApplicationDbContext context,
            IAssetRepository assetRepo) : base(repository)
        {
            _repository = repository;
            _fileService = fileService;
            _context = context;
            _assetRepo = assetRepo;
        }



        // --------------------------
        // CREATE (with Poster upload)
        // --------------------------
        public async Task<Guid> CreateFile(IngestJobRequest request)
        {
            // 1) Map request → IngestJob
            var job = request.Adapt<IngestJob>();
            

            // 2) Upload the video (IFormFile) the same way you upload images
            var videoPath = await _fileService.UploadAsyncVid(request.SourceUrl);
            job.SourceUrl = videoPath; // link to uploaded file

            // 3) Create Asset for playback
            var asset = new Asset
            {
                TitleId = request.TitleId,
                EpisodeId = request.EpisodeId,
               
                VideoUrl = videoPath,
              
            };
            await _assetRepo.AddAsync(asset);

            // 4) Complete the job
            job.CompletedAt = DateTime.UtcNow;

            // 5) Save IngestJob
            await _repository.AddAsync(job);

            return job.Id;
        }

        public async Task<IngestJobResponse> UpdateAsync(Guid id, UpdateIngestJobRequest request)
        {
            var job = await _repository.GetByIdAsync(id);
            if (job == null)
                throw new Exception("Ingest job not found");

            // Update normal fields
            job.TitleId = request.TitleId;
            job.EpisodeId = request.EpisodeId;
            job.Status = request.Status;
            job.UpdatedAt = DateTime.UtcNow;

            // If video uploaded → delete old + upload new one
            if (request.SourceUrl != null)
            {
                // Delete old file if exists
                if (!string.IsNullOrEmpty(job.SourceUrl))
                {
                    _fileService.Delete(job.SourceUrl);
                }

                // Upload new file
                var newVideoPath = await _fileService.UploadAsyncVid(request.SourceUrl);
                job.SourceUrl = newVideoPath;

                // Update related asset
                var assets = await _assetRepo.GetWhere(a =>
                    a.TitleId == job.TitleId &&
                    a.EpisodeId == job.EpisodeId);
                var asset = assets.FirstOrDefault();

                if (asset != null)
                {
                    // delete old asset file
                    if (!string.IsNullOrEmpty(asset.VideoUrl))
                    {
                        _fileService.Delete(asset.VideoUrl);
                    }

                    asset.VideoUrl = newVideoPath;
                    await _assetRepo.UpdateAsync(asset);
                }
            }

            await _repository.UpdateAsync(job);

            return job.Adapt<IngestJobResponse>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var job = await _repository.GetByIdAsync(id);
            if (job == null)
                return false;

            // 1) Delete ingest video file
            if (!string.IsNullOrEmpty(job.SourceUrl))
                _fileService.Delete(job.SourceUrl);

            // 2) Delete ALL assets linked to this ingest job (by title + episode)
            var assets = await _assetRepo.GetWhere(a =>
                a.TitleId == job.TitleId &&
                a.EpisodeId == job.EpisodeId);

            foreach (var asset in assets)
            {
                // remove each asset's video
                if (!string.IsNullOrEmpty(asset.VideoUrl))
                    _fileService.Delete(asset.VideoUrl);

                await _assetRepo.RemoveAsync(asset);
            }

            // 3) Delete the ingest job record itself
            await _repository.RemoveAsync(job);

            return true;
        }




    }
}
