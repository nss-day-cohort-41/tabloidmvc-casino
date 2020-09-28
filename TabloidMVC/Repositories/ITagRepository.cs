using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();
        void AddTag(Tag tag);
        void Edit(Tag tag);
        Tag GetTagById(int id);
        void Delete(int id);

        //void DeleteTag(int tagId);
        //void UpdateTag(Tag tag);

        Post GetPostById(int id);

        //Tag AddTagToPost(Tag tag, Post TagId);

        //Tag GetTagByUserId(int id, int userProfileId);
    }
}