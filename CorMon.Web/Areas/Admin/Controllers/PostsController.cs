using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CorMon.Application.Posts.Dto;
using CorMon.Application.Posts;
using CorMon.Core.Enums;

namespace CorMon.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : BaseController
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
            var model = new PostInput
            {
                ActionName= "Create",
                CreateDateTime=DateTime.Now,
                ModifiedDateTime=DateTime.Now,
                PublishDateTime=DateTime.Now,
                PublishStatus=PublishStatus.Draft,
                MetaRobots= RobotsState.Global,

            };
            return View(model);
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

            input.UserId = "599b295c03a89924849735b3";
            await _postService.InsertAsync(input);

            return RedirectToAction("index");
        }

        #endregion
    }
}