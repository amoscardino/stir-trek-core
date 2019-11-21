using System;
using System.Collections.Generic;
using System.Text;

namespace StirTrekCore.Models
{
    public class SessionModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Speaker> Speakers { get; set; }
        public string ScheduledRoom { get; set; }
        public string Track { get; set; }
        public List<Theatre> Theatres { get; set; }

        public bool IsSaved { get; set; }
    }
}
