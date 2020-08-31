using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventFinder.Models.Entity
{
    public class EventUser
    {
        public virtual Event Event { get; set; }
        public int EventId { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }

    }
}
