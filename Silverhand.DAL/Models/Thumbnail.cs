using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Models
{
    public class Thumbnail : Base
    {
        // Linked to the general title (Movie/Series)
        public Guid? TitleId { get; set; }
        public Title? Title { get; set; }

        // Required: every episode must have a thumbnail
        public Guid EpisodeId { get; set; }
        public Episode Episode { get; set; }

        // Image file location
        public string ImageUrl { get; set; } = default!;

        // Optional: If in the future you want scene-based thumbnails (00:10, 00:20, etc)
        public int? SceneSecond { get; set; }
    }

}
