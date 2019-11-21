using System;
using System.Collections.Generic;
using System.Text;

namespace StirTrekCore.Models
{
    public class Session
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Guid> Speakers { get; set; }
        public List<long> CategoryItems { get; set; }
        public string ScheduledRoom { get; set; }
    }
}
