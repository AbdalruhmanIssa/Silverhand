using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Requests
{
    public class EpisodeRequest
    {
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DurationSeconds { get; set; }
        public Guid TitleId { get; set; }


    }
}
