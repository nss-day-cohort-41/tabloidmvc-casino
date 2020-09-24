using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
   public interface ICommentRepository
    {
        List<Comment> GetCommentsByPostId(int id);
       // Comment GetCommentById(int id);
        void AddComment(Comment comment);
        //void Edit(Comment comment);
        //void Delete(Comment comment);

    }

}
