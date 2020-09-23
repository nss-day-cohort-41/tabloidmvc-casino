using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();

        Tag GetTagById(int id);
        void AddTag(Tag tag);

        /*Tag GetTagByPostId(int id);

        Tag GetTagByUserId(int id, int userProfileId);*/
    }
}