using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Requests
{
    public class FavoriteRequest
    {
        public Guid ProfileId { get; set; }
        public Guid TitleId { get; set; }
    }

}
