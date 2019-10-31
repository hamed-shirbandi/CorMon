using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CorMon.Application.Posts;
using CorMon.Application.Posts.Dto;
using CorMon.Core.Helpers;
using RedisCache.Core;
using CorMon.Core.JsonModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CorMon.Web.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : BaseController
    {
        #region Fields

        private readonly IPostService _postService;
        private readonly IRedisCacheService _redisCacheService;


        #endregion

        #region Ctor

        public PostsController(IPostService postService, IRedisCacheService redisCacheService)
        {
            _postService = postService;
            _redisCacheService = redisCacheService;

        }


        #endregion

        #region Public Methods




        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [Route("posts/{id}")]
        public PostOutput Get(string id)
        {
            var cacheKey = string.Format(CacheKeyTemplate.PostByIdCacheKey, id);
            return _redisCacheService.GetOrSet(key: cacheKey, factory: () => _postService.Get(id), cacheTimeInMinutes: 60);

        }




        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [Route("posts")]
        public async Task<PublicJsonResult> Post([FromBody] PostInput input)
        {
            if (!ModelState.IsValid)
            {
                return new PublicJsonResult { Result = false, Message = GetErrors(ModelState) };
            }

            return await _postService.CreateAsync(input);
        }



        /// <summary>
        /// 
        /// </summary>
        [HttpPut]
        [Route("posts")]
        public async Task<PublicJsonResult> Put([FromBody] PostInput input)
        {
            if (!ModelState.IsValid)
            {
                return new PublicJsonResult { Result = false, Message = GetErrors(ModelState) };
            }

            return await _postService.UpdateAsync(input);
        }




        /// <summary>
        /// 
        /// </summary>
        [HttpDelete]
        [Route("posts")]
        public async Task<PublicJsonResult> Delete(string id)
        {
            return await _postService.DeleteAsync(id);
        }




        #endregion

        #region  Private Methods


        /// <summary>
        /// 
        /// </summary>
        public string GetErrors(ModelStateDictionary modelState)
        {
            var getError = string.Empty;
            foreach (var value in modelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    getError += error.ErrorMessage + " - ";
                }
            }
            return getError;
        }



        #endregion

    }
}