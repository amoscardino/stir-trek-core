using System.Collections.Generic;

namespace StirTrekCore.Models
{
    public class TimeSlotModel
    {
        public List<SessionModel> Sessions { get; set; }
        public string Time { get; set; }
    }
}
