
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class CategoryDeleteViewModel
    {
        public Category Category { get; set; }
        public List<Post> Posts { get; set; }
    }
}
