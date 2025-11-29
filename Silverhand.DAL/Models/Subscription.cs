using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Models
{
    public class Subscription
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }
        public Guid PlanId { get; set; }

        public SubscriptionStatus Status { get; set; }

        public DateTime PeriodStartUtc { get; set; }
        public DateTime PeriodEndUtc { get; set; }

        public bool CancelAtPeriodEnd { get; set; }

        public string Provider { get; set; } = "Local";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ApplicationUser User { get; set; }
        public Plan Plan { get; set; }
    }
    public enum SubscriptionStatus
    {
        Trialing = 1,
        Active = 2,
        Canceled = 3,
        Expired = 4
    }
}
