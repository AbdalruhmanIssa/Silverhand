using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Responses
{
    public class SubtitleResponse
    {
        public Guid Id { get; set; }
        public Guid TitleId { get; set; }
        public Guid? EpisodeId { get; set; }
        // Navigation (optional but recommended)
        // Subtitle info
        public string LanguageCode { get; set; } 
        public string FileUrl { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}
