using EventFinder.Models.Entity;
using System.Collections.Generic;

namespace EventFinder.Models.ProfileModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }

        public List<Forum> Forums { get; set; }

        public List<Event> Events { get; set; }

        public List<EventUser> EventUser { get; set; }
    }
}
