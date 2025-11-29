using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Interface
{
    public interface ISubscriptionService
    {
        Task<SubscriptionResponse> SubscribeAsync(Guid userId, SubscriptionRequest request);
        Task<bool> CancelAsync(Guid userId);
        Task<SubscriptionResponse?> GetCurrentAsync(Guid userId);
    }
}
