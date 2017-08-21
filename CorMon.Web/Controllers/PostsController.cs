using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CorMon.Application.Posts.Dto;
using CorMon.Application.Posts;

namespace CorMon.Web.Controllers
{
    public class PostsController : Controller
    {
        #region Fields

        private readonly IPostService _postService;


        #endregion

        #region Ctor

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        #endregion

        #region Methods


        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.SearchAsync(term: "");

            return View(posts);
        }



        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }




        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(PostInput input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            await _postService.InsertAsync(input);

            return RedirectToAction("index");
        }

        #endregion
    }
}