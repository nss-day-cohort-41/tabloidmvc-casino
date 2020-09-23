using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    { 
      
       public CommentRepository(IConfiguration config) :base(config) { }
        public void Add(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO COMMENT (Subject, Content)
                         OUTPUT INSERTED.ID
                          VALUES (@Subject, @Content)";
                    cmd.Parameters.AddWithValue("@Subject", comment.Subject);
                    cmd.Parameters.AddWithValue("@Content", comment.Content);

                    comment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
