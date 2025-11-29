using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Models
{
    public class AvailabilityWindow : Base
    {
        public Guid TitleId { get; set; }
        public Guid? EpisodeId { get; set; }

        public string RegionCode { get; set; } = default!; // e.g. "US", "UK", "SA", "PS"

        public DateTime StartsAtUtc { get; set; }
        public DateTime EndsAtUtc { get; set; }

        public string? Notes { get; set; }
    }
}
