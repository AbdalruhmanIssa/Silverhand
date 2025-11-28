using Silverhand.BLL.Services.Interface;
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
    public class PlanService
       : GenericService<PlanRequest, PlanResponse, Plan>, IPlanService
    {
        private readonly IPlanRepository _repo;

        public PlanService(IPlanRepository repo)
            : base(repo)
        {
            _repo = repo;
        }

        public async Task<int> DeactivateAsync(Guid id)
        {
            var plan = await _repo.GetByIdAsync(id);
            if (plan is null) return 0;

            plan.IsActive = false;
            plan.UpdatedAt = DateTime.UtcNow;

            return await _repo.UpdateAsync(plan);
        }
    }
}
