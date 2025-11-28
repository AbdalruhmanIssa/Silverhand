using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Models
{
    public class Favorite 
    {
        public Guid Id { get; set; }

        public Guid ProfileId { get; set; }
        public Profile Profile { get; set; }

        public Guid TitleId { get; set; }
        public Title Title { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
