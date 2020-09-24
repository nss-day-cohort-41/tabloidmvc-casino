using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Security.Claims;
using TabloidMVC.Models;
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

        public ActionResult Delete(int id)
        {
            Tag tag = _tagRepository.GetTagById(id);
            return View(tag);
        }

        // POST: Tag/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Tag tag)
        {
            try
            {
                _tagRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(tag);
            }
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            try
            {
                _tagRepository.AddTag(tag);

                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                return View(tag);
            }
        }

        public IActionResult Edit(int id)
        {
            Tag tag = _tagRepository.GetTagById(id);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Tag tag)
        {
            try
            {
                _tagRepository.AddTag(tag);

                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                return View(tag);
            }
        }


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
