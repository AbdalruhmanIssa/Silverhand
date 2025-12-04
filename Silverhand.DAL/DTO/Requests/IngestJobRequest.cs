using Microsoft.AspNetCore.Http;
using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Requests
{
    public class IngestJobRequest
    {

        public Guid TitleId { get; set; }
        public Guid? EpisodeId { get; set; }
        // Where the raw file is located BEFORE processing
        public IFormFile SourceUrl { get; set; } 

        // JSON result (optional) – can hold logs or generated files
        public string? ResultJson { get; set; }

        // Enum is better than string
        public IngestStatus Status { get; set; } = IngestStatus.Pending;
        public AssetQuality Quality { get; set; }


        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
