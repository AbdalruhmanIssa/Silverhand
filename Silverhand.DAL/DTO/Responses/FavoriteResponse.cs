using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Responses
{
    public class FavoriteResponse
    {
        public Guid Id { get; set; }
        public Guid TitleId { get; set; }
        public string TitleName { get; set; }
        public string PosterUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
