using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Updates
{
    public class UpdateThumbnailRequest
    {
        public Guid EpisodeId { get; set; }     // optional — remove if you don't want to update it

        public IFormFile? ImageUrl { get; set; }  // optional

        public int? SceneSecond { get; set; }
    }

}
