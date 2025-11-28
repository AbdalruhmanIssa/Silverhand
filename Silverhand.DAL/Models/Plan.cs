using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Models
{
    public class Plan : Base
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Currency { get; set; } = "USD";
        public BillingInterval Interval { get; set; }
        public string MaxResolution { get; set; } = "1080p";
        public int MaxConcurrentStreams { get; set; }
        public bool AdsEnabled { get; set; }
        public bool IsActive { get; set; } = true;

        
    }
    public enum BillingInterval
    {
        Monthly = 1,
        Yearly = 2
    }

}
