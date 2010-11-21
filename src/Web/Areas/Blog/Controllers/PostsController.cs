﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyt.Core.DAL.Extensions;
using Trackyt.Core.DAL.Repositories;
using Web.Areas.Blog.Models;
using Web.Helpers.Extensions;
using Trackyt.Core.DAL.DataModel;

namespace Web.Areas.Blog.Controllers
{
    public class PostsController : Controller
    {
        private static readonly int PostsOnPageCount = 5;

        private IBlogPostsRepository _repository;

        public PostsController(IBlogPostsRepository postsRepository)
        {
            _repository = postsRepository;
        }

        public ActionResult Index()
        {
            var model = GetPostsByPage(1);

            return View(model);
        }

        public ActionResult Page(int id)
        {
            var model = GetPostsByPage(id);

            return View("Index", model);
        }

        public ActionResult PostByUrl(string url)
        {
            var model = GetPostByUrl(url);

            return View(model);
        }

        private BlogPost GetPostByUrl(string url)
        {
            return _repository.BlogPosts.WithUrl(url);            
        }

        private BlogPosts GetPostsByPage(int currentPage)
        {
            var posts = _repository.BlogPosts.Page(currentPage, PostsOnPageCount);
            var model = new BlogPosts(currentPage, TotalPages(), posts.ToList());
            return model;
        }

        private int TotalPages()
        {
            var postsCount = _repository.BlogPosts.Count();
            var delta = (postsCount % PostsOnPageCount) == 0 ? 0 : 1; 

            return (postsCount / PostsOnPageCount) + delta;
        }

    }
}
