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
    public interface IPlanService
        : IGenericService<PlanRequest, PlanResponse, Plan>
    {
        // If you want custom methods later (deactivate, etc.)
        Task<int> DeactivateAsync(Guid id);
    }
}
