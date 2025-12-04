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
    public interface IIngestJobService:IGenericService<IngestJobRequest, IngestJobResponse,IngestJob>
    {
        Task<Guid> CreateFile(IngestJobRequest request);
        Task<IngestJobResponse> UpdateAsync(Guid id, UpdateIngestJobRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
