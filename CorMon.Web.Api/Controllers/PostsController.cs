using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CorMon.Application.Posts;
using CorMon.Application.Posts.Dto;

namespace CorMon.Web.Api.Controllers
{
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

        #region Public Methods




        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [Route("posts/get/{id}")]
        public async Task<PostOutput> Get(string id)
        {
            return await _postService.GetAsync(id); 
        }




        #endregion

        #region  Private Methods




        #endregion

    }
}