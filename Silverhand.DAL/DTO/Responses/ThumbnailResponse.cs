using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Responses
{
    public class ThumbnailResponse
    {
        public Guid Id { get; set; }
        public Guid TitleId { get; set; }
      

        // Required: every episode must have a thumbnail
        public Guid EpisodeId { get; set; }
       

        // Image file location
        public string ImageUrl { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        // Optional: If in the future you want scene-based thumbnails (00:10, 00:20, etc)
        public int? SceneSecond { get; set; }
    }
}
