using Mapster;
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
    public class SubtitleService:GenericService<SubtitleRequest,SubtitleResponse, Subtitle>,ISubtitleService
    {
        private readonly ISubtitleRepository _repository;
        public SubtitleService(ISubtitleRepository repository):base(repository) {
            _repository = repository;
        }
        public async Task<List<SubtitleResponse>> GetAllAsync(Guid titleId, Guid? episodeId)
        {
            var subtitles = await _repository.GetWhere(s =>
    s.TitleId == titleId &&
    (episodeId == null ? s.EpisodeId == null : s.EpisodeId == episodeId));


            return subtitles.Adapt<List<SubtitleResponse>>();
        }

    }
}
