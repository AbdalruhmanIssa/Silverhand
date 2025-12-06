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
    public interface IAssetService : IGenericService<AssetRequest,AssetResponse,Asset>
    {
        Task<List<AssetResponse>> GetAllAsync(HttpRequest request);
        Task<AssetResponse> GetByIdAsync(Guid id, HttpRequest request);
        Task<AssetResponse> UpdateAsync(Guid id, UpdateAssetRequest request);
        Task<AssetResponse> CreateAssetAsync(AssetRequest request);
    }
}
