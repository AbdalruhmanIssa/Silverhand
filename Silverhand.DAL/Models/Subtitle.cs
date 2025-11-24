using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Models
{
    public class Subtitle:Base
    {
      

        public Guid TitleId { get; set; }
        public Title Title { get; set; }

        public Guid? EpisodeId { get; set; }
        public Episode? Episode { get; set; }

        public string LanguageCode { get; set; } // "en", "ar", "fr"
        public string FileUrl { get; set; }

   
    }

}
