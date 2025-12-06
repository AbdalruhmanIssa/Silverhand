using Azure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.Data;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using Silverhand.DAL.Models;
using Silverhand.DAL.Repository.Classes;
using Silverhand.DAL.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Classes
{
    public class EpisodeService : GenericService<EpisodeRequest, EpisodeResponse, Episode>, IEpisodeService
    {
        private readonly IEpisodeRepository repository;
        private readonly ITitleRepository _titleRepository;
        
        public EpisodeService(IEpisodeRepository repository,ITitleRepository titleRepository) : base(repository) {
            this.repository = repository;
            _titleRepository = titleRepository;
             


        }
        public async Task<Guid> CreateAsync(EpisodeRequest request)
        {
            // 1) Validate TitleId
            var title = await _titleRepository.GetByIdAsync(request.TitleId);
            if (title == null)
                throw new Exception("Invalid TitleId – Title does not exist.");

            // 2) Check if same Season & Episode exists
            var duplicate = await repository.GetWhere(e =>
                e.TitleId == request.TitleId &&
                e.SeasonNumber == request.SeasonNumber &&
                e.EpisodeNumber == request.EpisodeNumber
            );

            if (duplicate.Any())
                throw new Exception("Episode with same season and episode number already exists.");

            // 3) Auto-generate name if empty
            var finalName = request.Name;
            if (string.IsNullOrWhiteSpace(finalName))
            {
                finalName = $"{title.Name} S{request.SeasonNumber:00}E{request.EpisodeNumber:00}";
            }

            // 4) Map and save
            var entity = request.Adapt<Episode>();
            entity.Name = finalName;
            entity.CreatedAt = DateTime.UtcNow;

            await repository.AddAsync(entity);
            

            return entity.Id;
        }



    }
}