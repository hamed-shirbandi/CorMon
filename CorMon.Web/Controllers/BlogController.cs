using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CorMon.Application.Posts;
using CorMon.Core.Enums;
using CorMon.Application.Taxonomies;
using CorMon.Application.Posts.Dto;
using RedisCache.Core;
using CorMon.Core.Helpers;

namespace CorMon.Web.Controllers
{
    public class BlogController : BaseController
    {
        #region Fields

        private readonly IPostService _postService;
        private readonly ITaxonomyService _taxonomyService;
        private readonly IRedisCacheService _redisCacheService;
        private int recordsPerPage;


        #endregion

        #region Ctor

        public BlogController(IPostService postService, ITaxonomyService taxonomyService, IRedisCacheService redisCacheService)
        {
            _postService = postService;
            _taxonomyService = taxonomyService;
            _redisCacheService = redisCacheService;
            recordsPerPage = 5;

        }

        #endregion


        #region Public Methods






        /// <summary>
        /// نمایش لیست  ‍‍‍‍پست ها
        /// </summary>
        [HttpGet]
        [Route("articles/{taxonomyType?}/{taxonomyId?}/{taxonomyName?}")]
        public async Task<ActionResult> Articles(string taxonomyId = null, TaxonomyType? taxonomyType = null, string taxonomyName = "")
        {
            var cacheKey = string.Format(CacheKeyTemplate.PostsSearchCacheKey, 0, recordsPerPage, taxonomyId, taxonomyType);

            if (!_redisCacheService.TryGetValue(key: cacheKey, result: out IEnumerable<PostOutput> posts))
            {
                posts = await _postService.SearchAsync(page: 0, recordsPerPage: recordsPerPage, term: "", taxonomyId: taxonomyId, taxonomyType: taxonomyType, publishStatus: PublishStatus.Publish, sortOrder: SortOrder.Desc);
                await _redisCacheService.SetAsync(key: cacheKey, data: posts, cacheTimeInMinutes: 60);
            }

            #region ViewBags

            ViewBag.TaxonomyName = taxonomyName;


            #endregion

            return View(posts);
        }





        /// <summary>
        /// جستجوی پست ها
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> SearchArticles(int page = 1, string taxonomyId = null, TaxonomyType? taxonomyType = null, string taxonomyName = "")
        {
            var cacheKey = string.Format(CacheKeyTemplate.PostsSearchCacheKey, page, recordsPerPage, taxonomyId, taxonomyType);
           
            if (!_redisCacheService.TryGetValue(key: cacheKey, result: out IEnumerable<PostOutput> posts))
            {
                posts = await _postService.SearchAsync(page: page, recordsPerPage: recordsPerPage, term: "", taxonomyId: taxonomyId, taxonomyType: taxonomyType, publishStatus: PublishStatus.Publish, sortOrder: SortOrder.Desc);
                await _redisCacheService.SetAsync(key: cacheKey, data: posts, cacheTimeInMinutes: 60);
            }

            if (posts == null || !posts.Any())
                return Content("no-more-info");

            ViewBag.IsAjaxRequest = true;
            return PartialView("_ArticlesList", posts);

        }






        /// <summary>
        /// نمایش جزییات مطلب
        /// </summary>
        [Route("article/{id}/{title}")]
        public ActionResult Article(string id, string title)
        {
            var cacheKey = string.Format(CacheKeyTemplate.PostByIdCacheKey, id);
            var post =  _redisCacheService.GetOrSet(key: cacheKey, factory: () => _postService.Get(id), cacheTimeInMinutes: 60);

            return View(post);

        }







        #endregion

    }
}