using Microsoft.AspNetCore.Http;
using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Updates
{
    public class UpdateTitleRequest
    {
        public string Name { get; set; } = null!;
        public TitleType Type { get; set; }
        public string Description { get; set; } = null!;
        public int ReleaseYear { get; set; }

        public IFormFile? PosterImage { get; set; }
    }
}
