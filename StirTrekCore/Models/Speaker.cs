using System;
using System.Collections.Generic;

namespace StirTrekCore.Models
{
    public class Speaker
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string TagLine { get; set; }
        public string Bio { get; set; }
        public Uri ProfilePicture { get; set; }
        public List<Link> Links { get; set; }
    }

}
