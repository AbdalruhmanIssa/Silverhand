using Azure;
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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Classes
{
    public class EpisodeService : GenericService<EpisodeRequest, EpisodeResponse, Episode>, IEpisodeService
    {
        private readonly IEpisodeRepository repository;
        public EpisodeService(IEpisodeRepository repository) : base(repository) {
            this.repository = repository;

        }
    
    

    }
}