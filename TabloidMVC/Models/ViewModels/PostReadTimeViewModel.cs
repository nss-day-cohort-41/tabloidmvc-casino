using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class PostReadTimeViewModel
    {
        public Post Post { get; set; }
        public string ReadTime { get; set; }

        public Tag Tag { get; set; }
    }

}
