using System.Collections.Generic;

namespace StirTrekCore.Models
{
    public class SessionData
    {
        public List<Session> Sessions { get; set; }
        public List<Speaker> Speakers { get; set; }
        public List<object> Questions { get; set; }
        public List<Category> Categories { get; set; }
        public List<object> Rooms { get; set; }
    }

}
