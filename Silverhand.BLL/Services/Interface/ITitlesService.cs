using Microsoft.AspNetCore.Http;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using Silverhand.DAL.DTO.Updates;
using Silverhand.DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Interface
{
    public interface ITitlesService:IGenericService<TitleRequest, TitleResponse,Title>
    {
        Task<Guid> CreateFile(TitleRequest request);
        Task<List<TitleResponse>> GetTitles(HttpRequest request, bool onlyActive = false, int pagenum = 1, int pagesize = 1);
        Task<TitleResponse> UpdateAsync(Guid id, UpdateTitleRequest request);
        Task<bool> DeleteAsync(Guid id);
        Task<TitleResponse> GetByIdTitle(Guid id, HttpRequest httpRequest);
    }
}
