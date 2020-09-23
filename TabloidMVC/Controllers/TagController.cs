using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;
        //private readonly ICategoryRepository _categoryRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IActionResult Index()
        {
            var tags = _tagRepository.GetAllTags();
            return View(tags);
        }

        /*public IActionResult Create()
        {
            var vm = new TagCreateViewModel();
            vm.TagOptions = _categoryRepository.GetAll();
            return View(vm);
        }*/

        /*[HttpPost]
        public IActionResult Create(TagCreateViewModel vm)
        {
            try
            {
                vm.Tag.CreateDateTime = DateAndTime.Now;
                vm.Tag.IsApproved = true;
                vm.Tag.UserProfileId = GetCurrentUserProfileId();

                _tagRepository.Add(vm.Tag);

                return RedirectToAction("Details", new { id = vm.Tag.Id });
            }
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                return View(vm);
            }
        }*/

        /*private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }*/
    }
}
