using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CorMon.Application.Posts.Dto;
using CorMon.Application.Posts;
using CorMon.Core.Enums;
using CorMon.Application.Taxonomies;

namespace CorMon.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : BaseController
    {
        #region Fields

        private readonly IPostService _postService;
        private readonly ITaxonomyService _taxonomyService;

        
        #endregion

        #region Ctor

        public PostsController(IPostService postService, ITaxonomyService taxonomyService)
        {
            _postService = postService;
            _taxonomyService = taxonomyService;
        }

        #endregion

        #region Public Methods


        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.SearchAsync(term: "",publishStatus:null,sortOrder:SortOrder.Desc);

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
                ActionName = "Create",
                CreateDateTime = DateTime.Now,
                ModifiedDateTime = DateTime.Now,
                PublishDateTime = DateTime.Now,
                PublishStatus = PublishStatus.Draft,
                MetaRobots = RobotsState.Global,
                Categories =await _taxonomyService.GetCategoriesSelectListAsync(),
                TagsPrefill=new string[] {},
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
            await _postService.CreateAsync(input);

            return RedirectToAction("index");
        }





        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var post = await _postService.GetToUpdateAsync(id);
            post.Categories = await _taxonomyService.GetCategoriesSelectListAsync(post.CategoryIds);
            post.ActionName = "Update";
            return View(post);
        }





        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Update(PostInput input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            await _postService.UpdateAsync(input);

            return RedirectToAction("index");
        }






        #endregion
    }
}