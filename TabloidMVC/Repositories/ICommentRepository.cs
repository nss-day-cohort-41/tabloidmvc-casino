﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
   public interface ICommentRepository
    {
        List<Comment> GetCommentsByPostId(int id);
        List <Comment> GetCommentByUserProfileId(int userProfileId);
       // List<Comment> GetCommentById(int id);
        void AddComment(Comment comment);

        void DeleteComment(int commentId);
        Comment GetCommentById(int id); 

    }

}
