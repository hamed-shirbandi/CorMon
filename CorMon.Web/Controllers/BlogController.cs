using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CorMon.Application.Posts;
using CorMon.Core.Enums;

namespace CorMon.Web.Controllers
{
    public class BlogController : BaseController
    {
        #region Fields

        private readonly IPostService _postService;

        #endregion


        #region Ctor

        public BlogController( IPostService postService)
        {
            _postService = postService;
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