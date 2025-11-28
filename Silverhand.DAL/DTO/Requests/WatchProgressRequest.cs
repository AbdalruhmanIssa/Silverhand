using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WatchProgressRequest
{
    public Guid ProfileId { get; set; }
    public Guid TitleId { get; set; }
    public Guid? EpisodeId { get; set; }

    public int ProgressSeconds { get; set; }
    public int DurationSeconds { get; set; }
}
