using Microsoft.AspNetCore.Http;
using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Requests
{
    public class ThumbnailRequest
    {
       

        // Required: every episode must have a thumbnail
        public Guid EpisodeId { get; set; }
      

        // Image file location
        public IFormFile ImageUrl { get; set; } = default!;

        // Optional: If in the future you want scene-based thumbnails (00:10, 00:20, etc)
        public int? SceneSecond { get; set; }
    }
}
