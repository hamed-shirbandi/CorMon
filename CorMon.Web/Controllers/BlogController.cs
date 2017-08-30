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


        #endregion

        #region Ctor

        public BlogController(IPostService postService, ITaxonomyService taxonomyService)
        {
            _postService = postService;
            _taxonomyService = taxonomyService;
        }

        #endregion


        #region Public Methods






        /// <summary>
        /// نمایش لیست  نقل قول ها
        /// </summary>
        [HttpGet]
        [Route("articles")]
        public async Task<ActionResult> Articles()
        {
            var posts = await _postService.SearchAsync(term: "", publishStatus: PublishStatus.Publish, sortOrder: SortOrder.Desc);

            return View(posts);
        }






 
        /// <summary>
        /// نمایش جزییات مطلب
        /// </summary>
        [Route("article/{id}/{title}")]
        public async Task<ActionResult> Article(string id, string title)
        {

            var post = await _postService.GetAsync(id);
   
            return View("ReadMore", post);

        }







        #endregion

    }
}