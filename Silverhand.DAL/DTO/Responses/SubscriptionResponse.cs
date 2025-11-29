using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Responses
{
    public class SubscriptionResponse
    {
        public Guid Id { get; set; }
        public Guid PlanId { get; set; }
        public string PlanName { get; set; }
        public decimal PlanPrice { get; set; }

        public DateTime PeriodStartUtc { get; set; }
        public DateTime PeriodEndUtc { get; set; }

        public bool CancelAtPeriodEnd { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}
