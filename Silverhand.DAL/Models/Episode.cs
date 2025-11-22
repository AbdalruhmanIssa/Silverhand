
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Models
{
    public class Episode:Base
    {
        

        public Guid TitleId { get; set; }
        public Title Title { get; set; }   // Navigation Property ✔️
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DurationSeconds { get; set; }
       
    }

}
