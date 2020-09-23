using Microsoft.AspNetCore.Connections.Features;
using Microsoft.Data.SqlClient;
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
        private readonly IConfiguration _config;
       public CommentRepository(IConfiguration config) :base(config) 
        {
            _config = config;
        }
        public new SqlConnection Connection 
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Comment> GetAllPosts()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT Id, Subject, Content, CreationDateTime,
                          FROM Comment 
                          LEFT JOIN UserProfile ON comment.UserProfileId = UserProfile.Id                          ";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Comment> comments = new List<Comment>();
                    while (reader.Read())
                    {
                        UserProfile userProfile = new UserProfile
                        {
                            FirstName = reader.GetString(reader.GetOrdinal("FristName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName"))
                        };
                        Comment comment = new Comment
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Content = reader.GetString(reader.GetOrdinal("Content"))
                           
                        };
                        comments.Add(comment);
                    }
                    reader.Close();
                    return comments;
                }
            }
        }

        public Comment GetCommentById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
