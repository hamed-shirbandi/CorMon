using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CorMon.Application.Posts;
using CorMon.Core.Enums;
using CorMon.Application.Taxonomies;

namespace CorMon.Web.Controllers
{
    public class BlogController : BaseController
    {
        #region Fields

        private readonly IPostService _postService;
        private readonly ITaxonomyService _taxonomyService;
        private int recordsPerPage;


        #endregion

        #region Ctor

        public BlogController(IPostService postService, ITaxonomyService taxonomyService)
        {
            _postService = postService;
            _taxonomyService = taxonomyService;
            recordsPerPage = 5;

        }

        #endregion


        #region Public Methods






        /// <summary>
        /// نمایش لیست  ‍‍‍‍پست ها
        /// </summary>
        [HttpGet]
        [Route("articles")]
        public async Task<ActionResult> Articles()
        {
            var posts = await _postService.SearchAsync(page: 0, recordsPerPage: recordsPerPage, term: "", publishStatus: PublishStatus.Publish, sortOrder: SortOrder.Desc);

            return View(posts);
        }








        /// <summary>
        /// جستجوی پست ها
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> SearchArticles(int page = 1, string term = "")
        {

            var posts = await _postService.SearchAsync(page: page, recordsPerPage: recordsPerPage, term: term, publishStatus: PublishStatus.Publish, sortOrder: SortOrder.Desc);

            if (posts == null || !posts.Any())
                return Content("no-more-info");

            ViewBag.IsAjaxRequest = true;
            return PartialView("_ArticlesList", posts);

        }






        /// <summary>
        /// نمایش جزییات مطلب
        /// </summary>
        [Route("article/{id}/{title}")]
        public async Task<ActionResult> Article(string id, string title)
        {

            var post = await _postService.GetAsync(id);
   
            return View(post);

        }







        #endregion

    }
}