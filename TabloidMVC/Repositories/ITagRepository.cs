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

        /*Tag GetTagByPostId(int id);

        Tag GetTagByUserId(int id, int userProfileId);*/
    }
}