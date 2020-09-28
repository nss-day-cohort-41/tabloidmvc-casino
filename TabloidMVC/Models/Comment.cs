using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace TabloidMVC.Models
{
    // these are the propertires of a comment
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public UserProfile UserProfile { get; set; }
        public int UserProfileId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
