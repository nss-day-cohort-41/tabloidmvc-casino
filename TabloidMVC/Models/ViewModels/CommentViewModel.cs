using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class CommentViewModel
    {
        // this post is an Object that is a property of comment view model class. this post object represents the single post you are looking at
       public Post Post { get; set; }
        // all the comment are inside the list. the list is a collection of the object. they are comments that are for a single post
       public List<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
       public UserProfile UserProfile { get; set; }
        
    }
}
