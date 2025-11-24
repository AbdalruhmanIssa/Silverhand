using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Models
{
    public class Asset : Base
    {
        // Relations
        public Guid TitleId { get; set; }
        public Guid? EpisodeId { get; set; }

        // Navigation (optional but recommended)
        public Title Title { get; set; }
        public Episode? Episode { get; set; }

        // Video info
        public AssetQuality Quality { get; set; }
        public string VideoUrl { get; set; } 

        // Optional internal storage (raw/original file)
    
    }
    public enum AssetQuality
    {
        P2160 = 2160,   // 4K UHD
        P1440 = 1440,   // 2K
        P1080 = 1080,   // Full HD
        P720 = 720,    // HD
        P480 = 480,    // SD
        P360 = 360,
        P240 = 240
    }

}
