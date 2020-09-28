using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class CommentController : Controller
    {
        // this is pulling the info from the repositories 
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserProfileRepository _userProfileRepository;
       
        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository,  IUserProfileRepository userProfileRepository )

        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userProfileRepository = userProfileRepository;
          
            
            
        }
        //Get
        public ActionResult PostWithComment(int id, int userProfileId)
        { 
            // getting the published post by Id
            Post post = _postRepository.GetPublishedPostById(id);

            // getting the list of comments by post id
            List<Comment> comments = _commentRepository.GetCommentsByPostId(post.Id);

            //List <Comment> comments = _commentRepository.GetCommentByUserProfileId(userProfileId);

            // vm = new vm
            CommentViewModel vm = new CommentViewModel()
            {
                //getting the post and comments?
                Post = post,
                Comments = comments,

            };

            //loop over the comments and get a user profile  on each comment
            // Comment is the Type. This says for each comment in comments. Running throught the list of comments
            foreach (Comment comment in comments){
                //set the user profile on each comment to = the user profile you got from the data base
                //the Data for user profile is in the class UserProfile. You have to set a var which i called user. 
                //Then go into the userProfileRepository and getUserProfileById. Then call comment.UserProfileId to get the user
                UserProfile user = _userProfileRepository.GetUserProfileById(comment.UserProfileId);
                //Set user to = comment.UserProfile to get the users name
                comment.UserProfile = user;
            }
            // returning the vm 
            return View(vm);
        }

        // GET: CommentController
        public ActionResult Index(int id)
        {
            // this is getting the post that have been published by there id
            var post = _postRepository.GetPublishedPostById(id);
            // this is getting the comments by the postid 
           var comments = _commentRepository.GetCommentsByPostId(id);
            // this is returning the comments on that post
            return View(comments);
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: CommentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Comment comment)
        {
            try
            {
                _commentRepository.AddComment(comment);
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View(comment);
            }
        }

        // GET: CommentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {

            Comment comment = _commentRepository.GetCommentById(id);
            return View(comment);


        }

        // POST: CommentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Comment comment)
        {
            try
            {
                _commentRepository.DeleteComment(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(comment);
      
            }
        }
    }
}
