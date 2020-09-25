﻿using System;
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
       
        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository, IUserProfileRepository userProfileRepository)

        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userProfileRepository = userProfileRepository;
            
            
        }
        //Get
        public ActionResult PostWithComment(int id)
        { 
            // getting the published post by Id
            Post post = _postRepository.GetPublishedPostById(id);
            UserProfile userProfile = _userProfileRepository.GetUserProfileById(id);
            
           
            // getting the list of comments by post id
            List<Comment> comments = _commentRepository.GetCommentsByPostId(post.Id);
            List<Comment> comments = _commentRepository.GetCommentByUserProfileId(userProfile.Id);
            
           
            // vm = new vm
            CommentViewModel vm = new CommentViewModel()
            {
                //getting the post and comments?
                Post = post,
                Comments = comments,
               
            };
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
            return View();
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
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
    }
}
