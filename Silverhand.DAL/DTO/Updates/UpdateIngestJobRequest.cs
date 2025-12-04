using Microsoft.AspNetCore.Http;
using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Updates
{
    public class UpdateIngestJobRequest
    {
        public Guid TitleId { get; set; }
        public Guid? EpisodeId { get; set; }

        // nullable — only update if provided
        public IFormFile? SourceUrl { get; set; }

        public IngestStatus Status { get; set; }
    }

}
