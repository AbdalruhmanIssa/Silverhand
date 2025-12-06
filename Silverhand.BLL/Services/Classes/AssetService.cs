using Mapster;
using Microsoft.AspNetCore.Http;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.Data;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using Silverhand.DAL.DTO.Updates;
using Silverhand.DAL.Models;
using Silverhand.DAL.Repository.Repositories;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Classes
{
    public class AssetService:GenericService<AssetRequest,AssetResponse, Asset>,IAssetService
    {
        private readonly IAssetRepository _repository;
        private readonly ITitleRepository _titleRepo;
        private readonly IEpisodeRepository _episodeRepo;
        private readonly IIngestJobRepository _ingestRepo;
      private readonly  IFileService _fileService;

        public AssetService(
    IAssetRepository repository,
    ITitleRepository titleRepo,
    IEpisodeRepository episodeRepo,
    IIngestJobRepository ingestRepo,IFileService fileService
) : base(repository)
        {
            _repository = repository;
            _titleRepo = titleRepo;
            _episodeRepo = episodeRepo;
            _ingestRepo = ingestRepo;
            _fileService = fileService;
        }

        public async Task<List<AssetResponse>> GetAllAsync(HttpRequest request)
        {
            var assets = await _repository.GetAllAsync();

            return assets.Select(a => new AssetResponse
            {
                Id = a.Id,
                TitleId = a.TitleId,
                EpisodeId = a.EpisodeId,
                Quality = a.Quality,
                

                VideoUrl = a.VideoUrl != null
                    ? $"{request.Scheme}://{request.Host}/vids/{a.VideoUrl}"
                    : null

            }).ToList();
        }

        public async Task<AssetResponse> GetByIdAsync(Guid id, HttpRequest request)
        {
            var asset = await _repository.GetByIdAsync(id);
            if (asset == null) return null;

            return new AssetResponse
            {
                Id = asset.Id,
                TitleId = asset.TitleId,
                EpisodeId = asset.EpisodeId,
                Quality = asset.Quality,
               

                VideoUrl = asset.VideoUrl != null
                    ? $"{request.Scheme}://{request.Host}/Vids/{asset.VideoUrl}"
                    : null
            };
        }
        public async Task<AssetResponse> UpdateAsync(Guid id, UpdateAssetRequest request)
        {
            var asset = await _repository.GetByIdAsync(id);
            if (asset == null)
                throw new Exception("Asset not found");

            // only update the quality
            asset.Quality = request.Quality;
            asset.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(asset);

            return asset.Adapt<AssetResponse>();
        }
        public async Task<AssetResponse> CreateAssetAsync(AssetRequest request)
        {
            // 1) Ensure title exists
            var title = await _titleRepo.GetByIdAsync(request.TitleId);
            if (title == null)
                throw new Exception("Title not found");

            // 2) If episode is provided → check it
            if (request.EpisodeId != null)
            {
                var episode = await _episodeRepo.GetByIdAsync(request.EpisodeId.Value);
                if (episode == null)
                    throw new Exception("Episode not found");
            }
            var existingAsset = await _repository.GetWhere(a =>
    a.TitleId == request.TitleId &&
    a.EpisodeId == request.EpisodeId &&
    a.Quality == request.Quality);

            if (existingAsset.Any())
                throw new Exception("This quality already exists for this title/episode.");

            // 3) Find ingest job for this title/episode
            var ingestJob = (await _ingestRepo.GetWhere(j =>
                j.TitleId == request.TitleId &&
                j.EpisodeId == request.EpisodeId))
                .FirstOrDefault();

            if (ingestJob == null)
                throw new Exception("Please upload a video first.");

            if (string.IsNullOrEmpty(ingestJob.SourceUrl))
                throw new Exception("Ingest job is missing video source file.");

            // 4) Create asset using the video from ingest
            var asset = new Asset
            {
                TitleId = request.TitleId,
                EpisodeId = request.EpisodeId,
                Quality = request.Quality,

                // THE VIDEO COMES FROM INGEST JOB
                VideoUrl = ingestJob.SourceUrl,
            };

            await _repository.AddAsync(asset);

            return asset.Adapt<AssetResponse>();
        }
      

    }
}
