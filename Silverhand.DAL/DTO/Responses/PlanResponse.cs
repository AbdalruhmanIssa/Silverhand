using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Responses
{
    public class PlanResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public BillingInterval Interval { get; set; }
        public string MaxResolution { get; set; }
        public int MaxConcurrentStreams { get; set; }
        public bool AdsEnabled { get; set; }
        public bool IsActive { get; set; }
    }

}
