using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CorMon.Application.Posts.Dto;
using CorMon.Core.Data;
using CorMon.Core.Domain;
using System.Linq;
using CorMon.Core.JsonModels;
using CorMon.Core.Extensions;
using CorMon.Resource;

namespace CorMon.Application.Posts
{
    public class PostService : IPostService
    {
        #region Fields

        private readonly IPostRepository _postRepository;


        #endregion



        #region Ctor

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public async Task<PostJsonResult> InsertAsync(PostInput input)
        {

            //بررسی یکتا بودن عنوان مطلب
            var existPost = await _postRepository.GetAsync( input.Title.Trim(), input.PostType);
            if (existPost != null)
                return new PostJsonResult { result = false, message = Messages.Post_Title_Already_Exist };

            //بررسی نامک -- url friendly
            input.UrlTitle = input.UrlTitle.IsNullOrEmptyOrWhiteSpace() ? input.Title.GenerateUrlTitle() : input.UrlTitle.GenerateUrlTitle();

            var post = new Post
            {
                Title=input.Title,
                Content=input.Content,
                CreateDateTime=DateTime.Now,
                ModifiedDate=DateTime.Now,
                PostLevel=input.PostLevel,
                PostType=input.PostType,
                MetaDescription=input.MetaDescription,
                MetaKeyWords=input.MetaKeyWords,
                PublishDateTime=input.PublishDateTime,
                PublishStatus=input.PublishStatus,
                RobotsState=input.RobotsState,
                UrlTitle=input.UrlTitle,
                ThumbnailTileUrl=input.ThumbnailTileUrl,
                ThumbnailUrl=input.ThumbnailUrl,
                UserId=input.UserId,
            };

            var savedPost= await _postRepository.InsertAsync(post);
            return new PostJsonResult { result=true,id=savedPost.Id,message=Messages.Post_InsertOne_Success };
        }




        /// <summary>
        /// 
        /// </summary>
        public async Task<IEnumerable<PostOutput>> SearchAsync(string term)
        {
            var posts = await _postRepository.SearchAsync(term);
            return posts.Select(post => 
            new PostOutput
            {
                Title=post.Title,
                Content=post.Content,
               // Author=post.User.DisplayName,

            }).ToList();
        }




        #endregion


    }
}
