using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CorMon.Application.Posts.Dto;
using CorMon.Application.Posts;
using CorMon.Core.Enums;
using CorMon.Application.Taxonomies;
using CorMon.Web.Helpers;
using CorMon.Web.Extensions;
using CorMon.Web.Enums;
using CorMon.Application.Users;
using Microsoft.AspNetCore.Authorization;

namespace CorMon.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class PostsController : BaseController
    {
        #region Fields

        private readonly IPostService _postService;
        private readonly ITaxonomyService _taxonomyService;
        private readonly IUserService _userService;
        private int recordsPerPage;
        private int pageSize;
        private int TotalItemCount;

        #endregion

        #region Ctor

        public PostsController(IUserService userService, IPostService postService, ITaxonomyService taxonomyService)
        {
            _postService = postService;
            _userService = userService;
            _taxonomyService = taxonomyService;
            pageSize = 0;
            recordsPerPage = 5;
            TotalItemCount = 0;
        }

        #endregion

        #region Public Methods


        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index(bool isTrashed = false)
        {
            var posts = _postService.Search(page: 1, recordsPerPage: recordsPerPage, term: "", isTrashed: isTrashed, publishStatus: null, sortOrder: SortOrder.Desc, pageSize: out pageSize, TotalItemCount: out TotalItemCount);

            #region ViewBags

            ViewBag.IsTrashed = isTrashed;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = 1;
            ViewBag.TotalItemCount = TotalItemCount;


            #endregion

            return View(posts);
        }





        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Search(int page = 1, string term = "", bool isTrashed = false, PublishStatus? publishStatus = null, SortOrder sortOrder = SortOrder.Desc)
        {
            var posts = _postService.Search(page: page, recordsPerPage: recordsPerPage, term: term, isTrashed: isTrashed, publishStatus: publishStatus, sortOrder: sortOrder, pageSize: out pageSize, TotalItemCount: out TotalItemCount);

            #region ViewBags

            ViewBag.IsTrashed = isTrashed;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = page;
            ViewBag.TotalItemCount = TotalItemCount;


            #endregion

            return PartialView("_PostList", posts);



        }






        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new PostInput
            {
                Categories = await _taxonomyService.GetCategoriesSelectListAsync()
            };


            return View(model);
        }







        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JavaScriptResult> Create(PostInput input)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetErrors();
                return ScriptBox.ShowMessage(errors, MsgType.error);

            }


            input.UserId = GetCurrentUserId();

            var createPost = await _postService.CreateAsync(input);
            if (!createPost.result)
                return ScriptBox.ShowMessage(createPost.message, MsgType.error);

            return ScriptBox.RedirectToUrl(url: "/admin/posts/update", values: new { id = createPost.id }, message: createPost.message);
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
                var errors = ModelState.GetErrors();
                return ScriptBox.ShowMessage(errors, MsgType.error);
            }

            var updatePost = await _postService.UpdateAsync(input);
            if (!updatePost.result)
                return ScriptBox.ShowMessage(updatePost.message, MsgType.error);

            return ScriptBox.ReloadPage();
        }





        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {

            var deletePost = await _postService.DeleteAsync(id);
            if (!deletePost.result)
                return ScriptBox.ShowMessage(deletePost.message, MsgType.error);

            return ScriptBox.RedirectToUrl(url: "/admin/posts");

        }



        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Recycle(string id)
        {

            var response = await _postService.RecycleAsync(id);
            if (!response.result)
                return ScriptBox.ShowMessage(response.message, MsgType.error);

            return ScriptBox.ReloadPage();
        }






        #endregion
    }
}