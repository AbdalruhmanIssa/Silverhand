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
        public SubtitleService(ISubtitleRepository repository):base(repository) {  }
    }
}
