using System.Collections.Generic;

namespace StirTrekCore.Models
{
    public class ScheduledSession
    {
        public List<TimeSlot> TimeSlots { get; set; }
        public string Day { get; set; }
    }
}
