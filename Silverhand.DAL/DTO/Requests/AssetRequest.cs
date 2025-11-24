using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Requests
{
    public class AssetRequest
    {
        public Guid TitleId { get; set; }
        public Guid? EpisodeId { get; set; }

        // Navigation (optional but recommended)
      

        // Video info
        public AssetQuality Quality { get; set; }
        public string VideoUrl { get; set; }
    }
}
