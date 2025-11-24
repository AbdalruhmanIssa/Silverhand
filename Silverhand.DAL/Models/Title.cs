using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Models
{
    public class Title : Base
    {
        public string Name { get; set; } = null!;

        // Movie / Series
        public TitleType Type { get; set; }

        public string Description { get; set; } = null!;

        public int ReleaseYear { get; set; }

        public string PosterUrl { get; set; } = null!;

        public string? BackdropUrl { get; set; }

        // Navigation for later
        public ICollection<Episode>? Episodes { get; set; }
        public ICollection<Asset> Assets { get; set; }
        public ICollection<Subtitle> Subtitles { get; set; } 
       

    }

    public enum TitleType
    {
        Movie = 1,
        Series = 2
    }
}
