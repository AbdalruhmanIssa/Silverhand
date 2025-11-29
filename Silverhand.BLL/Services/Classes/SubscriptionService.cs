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
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepo;
        private readonly IPlanRepository _planRepo;

        public SubscriptionService(
            ISubscriptionRepository subscriptionRepo,
            IPlanRepository planRepo)
        {
            _subscriptionRepo = subscriptionRepo;
            _planRepo = planRepo;
        }

        // -----------------------------------
        // SUBSCRIBE
        // -----------------------------------
        public async Task<SubscriptionResponse> SubscribeAsync(Guid userId, SubscriptionRequest request)
        {
            var plan = await _planRepo.GetByIdAsync(request.PlanId);
            if (plan == null || !plan.IsActive)
                throw new Exception("Plan not found or inactive.");

            var active = await _subscriptionRepo.GetActiveAsync(userId);
            if (active != null)
                throw new Exception("User already has an active subscription.");

            var sub = new Subscription
            {
                UserId = userId,
                PlanId = plan.Id,
                Status = SubscriptionStatus.Active,
                Provider = "Local",
                PeriodStartUtc = DateTime.UtcNow,
                PeriodEndUtc = DateTime.UtcNow.AddMonths(1),
                CreatedAt = DateTime.UtcNow
            };

            await _subscriptionRepo.AddAsync(sub);

            return new SubscriptionResponse
            {
                Id = sub.Id,
                PlanId = plan.Id,
                PlanName = plan.Name,
                PlanPrice = plan.Price,
                PeriodStartUtc = sub.PeriodStartUtc,
                PeriodEndUtc = sub.PeriodEndUtc,
                CancelAtPeriodEnd = sub.CancelAtPeriodEnd,
                Status = sub.Status
            };
        }

        // -----------------------------------
        // CANCEL SUBSCRIPTION
        // -----------------------------------
        public async Task<bool> CancelAsync(Guid userId)
        {
            var sub = await _subscriptionRepo.GetActiveAsync(userId);
            if (sub == null)
                throw new Exception("No active subscription found.");

            sub.CancelAtPeriodEnd = true;
            sub.Status = SubscriptionStatus.Canceled;
            sub.UpdatedAt = DateTime.UtcNow;

            await _subscriptionRepo.UpdateAsync(sub);

            return true;
        }

        // -----------------------------------
        // GET CURRENT SUBSCRIPTION
        // -----------------------------------
        public async Task<SubscriptionResponse?> GetCurrentAsync(Guid userId)
        {
            var sub = await _subscriptionRepo.GetActiveAsync(userId);
            if (sub == null)
                return null;

            var plan = sub.Plan; // Make sure EF loads Plan if needed

            return new SubscriptionResponse
            {
                Id = sub.Id,
                PlanId = sub.PlanId,
                PlanName = plan?.Name,
                PlanPrice = plan?.Price ?? 0,
                PeriodStartUtc = sub.PeriodStartUtc,
                PeriodEndUtc = sub.PeriodEndUtc,
                CancelAtPeriodEnd = sub.CancelAtPeriodEnd,
                Status = sub.Status
            };
        }
    }
}
