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
using CorMon.Core.Enums;
using CorMon.Application.Taxonomies.Dto;

namespace CorMon.Application.Posts
{
    public class PostService : IPostService
    {
        #region Fields

        private readonly IPostRepository _postRepository;
        private readonly ITaxonomyRepository _taxonomyRepository;
        private readonly IUserRepository _userRepository;


        #endregion

        #region Ctor

        public PostService(IPostRepository postRepository, IUserRepository userRepository, ITaxonomyRepository taxonomyRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _taxonomyRepository = taxonomyRepository;
        }


        #endregion

        #region Methods




        /// <summary>
        /// 
        /// </summary>
        public async Task<PostInput> GetToUpdateAsync(string id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                throw new Exception("Post not found");
            }

            return new PostInput
            {

                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                PostLevel = post.PostLevel,
                MetaDescription = post.MetaDescription,
                MetaKeyWords = post.MetaKeyWords,
                PublishDateTime = post.PublishDateTime,
                PublishStatus = post.PublishStatus,
                MetaRobots = post.MetaRobots,
                UrlTitle = post.UrlTitle,
                UserId = post.UserId,
                Author = post.Author,
                ModifiedDateTime = post.ModifiedDateTime,
                CreateDateTime = post.CreateDateTime,

            };
        }




        /// <summary>
        /// 
        /// </summary>
        public async Task<PostOutput> GetAsync(string id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null || post.IsDeleted)
            {
                throw new Exception("Post not found");
            }

            var user = await _userRepository.GetAsync(post.UserId);
            if (post == null || post.IsDeleted)
            {
                throw new Exception("User not found");
            }

            return new PostOutput
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                PostLevel = post.PostLevel,
                MetaDescription = post.MetaDescription,
                MetaKeyWords = post.MetaKeyWords,
                PublishDateTime = post.PublishDateTime,
                PublishStatus = post.PublishStatus,
                MetaRobots = post.MetaRobots,
                UrlTitle = post.UrlTitle,
                UserId = post.UserId,
                Author = user.DisplayName,
                AboutAuthor = user.About,
                ModifiedDateTime = post.ModifiedDateTime,
                Categoories = GetPostAllTaxonomies(post.CategoryIds),
                Tags = GetPostAllTaxonomies(post.CategoryIds),
            };
        }






        /// <summary>
        /// 
        /// </summary>
        public async Task<PostJsonResult> CreateAsync(PostInput input)
        {

            //بررسی یکتا بودن عنوان مطلب
            var existPost = await _postRepository.GetByTitleAsync(input.Title.Trim());
            if (existPost != null)
                return new PostJsonResult { result = false, message = Messages.Post_Title_Already_Exist };

            //بررسی نامک -- url friendly
            input.UrlTitle = input.UrlTitle.IsNullOrEmptyOrWhiteSpace() ? input.Title.GenerateUrlTitle() : input.UrlTitle.GenerateUrlTitle();

            var post = new Post
            {
                Title = input.Title,
                Author = input.Author,
                Content = input.Content,
                CreateDateTime = DateTime.Now,
                ModifiedDateTime = DateTime.Now,
                PostLevel = input.PostLevel,
                MetaDescription = input.MetaDescription,
                MetaKeyWords = input.MetaKeyWords,
                PublishDateTime = input.PublishDateTime,
                PublishStatus = input.PublishStatus,
                MetaRobots = input.MetaRobots,
                UrlTitle = input.UrlTitle,
                UserId = input.UserId,
            };

            await _postRepository.CreateAsync(post);
            return new PostJsonResult { result = true, id = post.Id, message = Messages.Post_Create_Success };
        }




        /// <summary>
        /// 
        /// </summary>
        public async Task<PostJsonResult> UpdateAsync(PostInput input)
        {
            var post = await _postRepository.GetByIdAsync(input.Id);
            if (post == null || post.IsDeleted)
            {
                throw new Exception("Post not found");
            }

            //بررسی یکتا بودن عنوان 
            var existPost = await _postRepository.GetByTitleAsync(input.Title.Trim());
            if (existPost != null && existPost.Id != input.Id)
                return new PostJsonResult { result = false, message = Messages.Post_Title_Already_Exist };


            post.UrlTitle = input.UrlTitle.IsNullOrEmptyOrWhiteSpace() ? input.Title.GenerateUrlTitle() : input.UrlTitle.GenerateUrlTitle();
            post.Title = input.Title;
            post.Content = input.Content;
            post.PublishDateTime = input.PublishDateTime;
            post.PublishStatus = input.PublishStatus;
            post.MetaDescription = input.MetaDescription;
            post.MetaKeyWords = input.MetaKeyWords;
            post.MetaRobots = input.MetaRobots;
            post.PostLevel = input.PostLevel;
            post.UrlTitle = input.UrlTitle;

            await _postRepository.UpdateAsync(post);
            return new PostJsonResult { result = true, message = Messages.Post_Update_Success };


        }







        /// <summary>
        /// 
        /// </summary>
        public async Task<IEnumerable<PostOutput>> SearchAsync(string term, PublishStatus? publishStatus, SortOrder sortOrder)
        {
            var posts = await _postRepository.SearchAsync(term, publishStatus, sortOrder);
            return posts.Select(post => new PostOutput
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Author = post.Author,
                PostLevel = post.PostLevel,
                MetaDescription = post.MetaDescription,
                MetaKeyWords = post.MetaKeyWords,
                PublishDateTime = post.PublishDateTime,
                PublishStatus = post.PublishStatus,
                MetaRobots = post.MetaRobots,
                UrlTitle = post.UrlTitle,
                UserId = post.UserId,
                Categoories = GetPostAllTaxonomies(post.CategoryIds),
                Tags = GetPostAllTaxonomies(post.CategoryIds),

            }).ToList();
        }









        #endregion


        #region Private Methods


        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<TaxonomyOutput> GetPostAllTaxonomies(string[] taxIds)
        {
            var taxs = _taxonomyRepository.GetListByIds(taxIds);
            return taxs.Select(tax => new TaxonomyOutput
            {
                Id = tax.Id,
                Name = tax.Name,
                Description = tax.Description,
                PostCount = tax.PostCount,
                Type = tax.Type,
                UrlTitle = tax.UrlTitle,
            });
        }





        #endregion

    }
}
