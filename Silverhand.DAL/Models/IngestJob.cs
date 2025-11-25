using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Models
{
    public class IngestJob : Base
    {
        public Guid TitleId { get; set; }
        public Title Title { get; set; }  // Navigation Property
        public Guid? EpisodeId { get; set; }
        public Episode? Episode { get; set; }  // Navigation Property

        // Where the raw file is located BEFORE processing
        public string SourceUrl { get; set; } = default!;

        // JSON result (optional) – can hold logs or generated files
        public string? ResultJson { get; set; }

        // Enum is better than string
        public IngestStatus Status { get; set; } = IngestStatus.Pending;

        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
    public enum IngestStatus
    {
        Pending = 0,
        Processing = 1,
        Completed = 2,
        Failed = 3
    }

}
