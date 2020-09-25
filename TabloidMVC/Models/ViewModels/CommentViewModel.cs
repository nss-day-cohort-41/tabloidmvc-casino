using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class CommentViewModel
    {
        // pulling all the data for post
       public Post Post { get; set; }
        // this is getting the list of comments
       public List<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
       public UserProfile UserProfile { get; set; }
        
    }
}
