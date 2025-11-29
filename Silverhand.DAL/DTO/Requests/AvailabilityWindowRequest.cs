using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Requests
{
    public class AvailabilityWindowRequest
    {
        public Guid TitleId { get; set; }
        public Guid? EpisodeId { get; set; }
        public string RegionCode { get; set; } = default!;
        public DateTime StartsAtUtc { get; set; }
        public DateTime EndsAtUtc { get; set; }
        public string? Notes { get; set; }
    }

}
