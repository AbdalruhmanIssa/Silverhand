using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Requests
{
    public class SubtitleRequest
    {
        public Guid TitleId { get; set; }
      

        public Guid? EpisodeId { get; set; }
       

        public string LanguageCode { get; set; } // "en", "ar", "fr"
        public string FileUrl { get; set; }
    }
}
