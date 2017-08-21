using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CorMon.Application.Posts.Dto;
using CorMon.Core.Data;
using CorMon.Core.Domain;
using System.Linq;

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
        public async Task InsertAsync(PostInput input)
        {
            var post = new Post
            {
                Title=input.Title,
                Content=input.Content
            };

           await _postRepository.InsertAsync(post);
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
