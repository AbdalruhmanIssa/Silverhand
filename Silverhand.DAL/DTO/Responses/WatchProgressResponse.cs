using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Responses
{
    public class WatchProgressResponse
    {
        public Guid Id { get; set; }

        public Guid ProfileId { get; set; }
        public Guid TitleId { get; set; }
        public Guid? EpisodeId { get; set; }

        public int ProgressSeconds { get; set; }
        public int DurationSeconds { get; set; }

        public bool Completed { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
