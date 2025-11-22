using Microsoft.AspNetCore.Http;
using StreamingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Responses
{
    public class TitleResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        // Movie / Series
        public TitleType Type { get; set; }

        public string Description { get; set; } = null!;

        public int ReleaseYear { get; set; }

        public string PosterUrl { get; set; } = null!;
     

        public string? BackdropUrl { get; set; }
    }
}
