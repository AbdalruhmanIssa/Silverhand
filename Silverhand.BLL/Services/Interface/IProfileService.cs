using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Interface
{
    public interface IProfileService
    {
        Task<ProfileResponse> CreateProfileAsync(Guid userId, ProfileRequest request);
        Task<IEnumerable<ProfileResponse>> GetAllByUserAsync(Guid userId);
        Task<bool> DeleteAsync(Guid profileId, Guid userId);
        Task<ProfileResponse?> UpdateAsync(Guid profileId, ProfileRequest request, Guid userId);

    }
}
