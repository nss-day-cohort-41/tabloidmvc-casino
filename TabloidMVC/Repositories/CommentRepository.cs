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
        public CommentRepository(IConfiguration config) : base(config) { }

       // this is getting the comments by postId. meaning it will show all comments tied to the post in which they are commenting on 
        public List<Comment> GetCommentsByPostId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT Id, Subject, Content, CreateDateTime, PostId, UserProfileId
                          FROM Comment 
                          WHERE PostId = @postId
                          ORDER BY CreateDateTime DESC
                                                  ";
                    cmd.Parameters.AddWithValue("@postId", id);
                    var reader = cmd.ExecuteReader();
                    var comments = new List<Comment>();
                  
                    while (reader.Read())
                    {
                      
                        comments.Add(new Comment()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId"))
                            
                        });

                    }
                    reader.Close();
                    return comments;
                }
            }
        }
        public void AddComment(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            INSERT INTO COMMENT (Subject, Content, CreateDateTime, UserProfileId, PostId)
                             OUTPUT INSERTED.ID
                              VALUES (@Subject, @Content)";
                    cmd.Parameters.AddWithValue("@Subject", comment.Subject);
                    cmd.Parameters.AddWithValue("@Content", comment.Content);
                    cmd.Parameters.AddWithValue("@CreateDateTime", comment.CreateDateTime);
                    cmd.Parameters.AddWithValue("@PostId", comment.PostId);
                    cmd.Parameters.AddWithValue("@UserProfileId", comment.UserProfileId);
                    comment.Id = (int)cmd.ExecuteScalar();
                }
            }

        }
    }
}