using System.Collections.Generic;

namespace StirTrekCore.Models
{
    public class TimeSlot
    {
        public List<TimeSlotSession> Sessions { get; set; }
        public string Time { get; set; }
    }
}
