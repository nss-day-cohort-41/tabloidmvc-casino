using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        public TagRepository(IConfiguration config) : base(config) { }
        public List<Tag> GetAllTags()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, name FROM Tag";
                    var reader = cmd.ExecuteReader();

                    var tags = new List<Tag>();

                    while (reader.Read())
                    {
                        tags.Add(new Tag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                        });
                    }

                    reader.Close();

                    return tags;
                }
            }
        }

        public Tag GetTagById(int tagId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name]
                        FROM Tag
                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", tagId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        var tag = new Tag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };

                        reader.Close();
                        return tag;
                    }

                    reader.Close();
                    return null;
                }
            }
        }

        public void AddTag(Tag tag)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Tag (Name)
                        OUTPUT INSERTED.ID
                        VALUES (@name)";

                    cmd.Parameters.AddWithValue("@name", tag.Name);

                    tag.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Tag
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void Edit(Tag tag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Tag
                            SET Name = @name
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@name", tag.Name);
                    cmd.Parameters.AddWithValue("@id", tag.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /* public Tag GetTagByPostId()
         {
             using (var conn = Connection)
             {
                 conn.Open();
                 using (var cmd = conn.CreateCommand())
                 {
                     cmd.CommandText = @"
                        SELECT p.Id, p.Title, p.Content, 
                               p.ImageLocation AS HeaderImage,
                               p.CreateDateTime, p.PublishDateTime, p.IsApproved,
                               p.CategoryId, p.UserProfileId,
                               c.[Name] AS CategoryName,
                               u.FirstName, u.LastName, u.DisplayName, 
                               u.Email, u.CreateDateTime, u.ImageLocation AS AvatarImage,
                               u.UserTypeId, 
                               ut.[Name] AS UserTypeName
                          FROM Post p
                               LEFT JOIN Category c ON p.CategoryId = c.id
                               LEFT JOIN UserProfile u ON p.UserProfileId = u.id
                               LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                         WHERE IsApproved = 1 AND PublishDateTime < SYSDATETIME()
                               AND p.id = @id";

                     cmd.Parameters.AddWithValue("@id", id);
                     var reader = cmd.ExecuteReader();

                     Post post = null;

                     if (reader.Read())
                     {
                         post = NewPostFromReader(reader);
                     }

                     reader.Close();

                     return post;
                 }
             }
         }

         public Tag GetTagByUserId(int id, int userProfileId)
         {
             using (var conn = Connection)
             {
                 conn.Open();
                 using (var cmd = conn.CreateCommand())
                 {
                     cmd.CommandText = @"
                        SELECT p.Id, p.Title, p.Content, 
                               p.ImageLocation AS HeaderImage,
                               p.CreateDateTime, p.PublishDateTime, p.IsApproved,
                               p.CategoryId, p.UserProfileId,
                               c.[Name] AS CategoryName,
                               u.FirstName, u.LastName, u.DisplayName, 
                               u.Email, u.CreateDateTime, u.ImageLocation AS AvatarImage,
                               u.UserTypeId, 
                               ut.[Name] AS UserTypeName
                          FROM Post p
                               LEFT JOIN Category c ON p.CategoryId = c.id
                               LEFT JOIN UserProfile u ON p.UserProfileId = u.id
                               LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                         WHERE p.id = @id AND p.UserProfileId = @userProfileId";

                     cmd.Parameters.AddWithValue("@id", id);
                     cmd.Parameters.AddWithValue("@userProfileId", userProfileId);
                     var reader = cmd.ExecuteReader();

                     Post post = null;

                     if (reader.Read())
                     {
                         post = NewPostFromReader(reader);
                     }

                     reader.Close();

                     return post;
                 }
             }
         }*/
    }
}
