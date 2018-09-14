using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using CorMon.Application.Posts;
using CorMon.Core.Enums;
using CorMon.Core.Helpers;
using RedisCache.Core;
using System.Collections.Generic;
using CorMon.Application.Posts.Dto;

namespace CorMon.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region Fields

        private readonly IPostService _postService;
        private readonly IRedisCacheService _redisCacheService;

        private int recordsPerPage;

        #endregion

        #region Ctor

        public HomeController(IPostService postService, IRedisCacheService redisCacheService)
        {
            _postService = postService;
            _redisCacheService = redisCacheService;
            recordsPerPage = 1;
        }


        #endregion

        #region Public Methods




        /// <summary>
        /// 
        /// </summary>
        public async Task<IActionResult> Index()
        {

            if (!_redisCacheService.TryGetValue(key: CacheKeyTemplate.PostsCacheKey, result: out IEnumerable<PostOutput> posts))
            {
                 posts = await _postService.SearchAsync(page: 0, recordsPerPage: recordsPerPage, term: "", taxonomyId: null, taxonomyType: null, publishStatus: PublishStatus.Publish, sortOrder: SortOrder.Desc);
                await _redisCacheService.SetAsync(key: CacheKeyTemplate.PostsCacheKey, data: posts, cacheTimeInMinutes: 60);
            }

            return View(posts);
        }






        /// <summary>
        /// use in ui tests
        /// </summary>
        public IActionResult Check_App_Initialization()
        {
            return View();
        }


        #endregion

    }
}
