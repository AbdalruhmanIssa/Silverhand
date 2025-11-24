using Microsoft.AspNetCore.Http;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using Silverhand.DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Interface
{
    public interface IThumbmailService:IGenericService<ThumbnailRequest, ThumbnailResponse,Thumbnail>
    {
        Task<Guid> CreateFile(ThumbnailRequest request);

    }
}
