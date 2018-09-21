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
using CorMon.Core.Helpers;
using CorMon.Application.Mapper;

namespace CorMon.Application.Posts
{
    public class PostService : IPostService
    {
        #region Fields

        private readonly IPostRepository _postRepository;
        private readonly ITaxonomyRepository _taxonomyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapperService _mapperService;


        #endregion

        #region Ctor

        public PostService(IPostRepository postRepository, IUserRepository userRepository, ITaxonomyRepository taxonomyRepository, IMapperService mapperService)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _taxonomyRepository = taxonomyRepository;
            _mapperService = mapperService;
        }


        #endregion

        #region Public Methods




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

            var tagsPrefill = GetPostTaxonomiesNameArray(post.TagIds);
            return _mapperService.BindToInputModel(post, tagsPrefill);
        }





        /// <summary>
        /// 
        /// </summary>
        public async Task<PostOutput> GetAsync(string id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null || post.IsTrashed)
            {
                throw new Exception("Post not found");
            }

            var user = await _userRepository.GetAsync(post.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var tags = GetPostTaxonomies(post.TagIds);
            var categories = GetPostTaxonomies(post.CategoryIds);

            return _mapperService.BindToOutputModel(post, user, tags, categories);
        }






        /// <summary>
        /// 
        /// </summary>
        public PostOutput Get(string id)
        {
            var post = _postRepository.GetByIdAsync(id).Result;
            if (post == null || post.IsTrashed)
            {
                throw new Exception("Post not found");
            }

            var user = _userRepository.GetAsync(post.UserId).Result;
            var tags = GetPostTaxonomies(post.TagIds);
            var categories = GetPostTaxonomies(post.CategoryIds);

            return _mapperService.BindToOutputModel(post, user, tags, categories);
        }






        /// <summary>
        ///  
        /// </summary>
        public async Task<PublicJsonResult> CreateAsync(PostInput input)
        {

            //بررسی یکتا بودن عنوان مطلب
            var existPost = await _postRepository.GetByTitleAsync(input.Title.Trim());
            if (existPost != null)
                return new PublicJsonResult { result = false, message = Messages.Post_Title_Already_Exist };

            //بررسی نامک -- url friendly
            input.UrlTitle = input.UrlTitle.IsNullOrEmptyOrWhiteSpace() ? input.Title.GenerateUrlTitle() : input.UrlTitle.GenerateUrlTitle();

            var tagsId = await AddTagsToPostAsync(input.Tags);
            var categoriesId = AddTagsToPost(input.Categories);

            var post = _mapperService.BindToDomainModel(input, categoriesId, tagsId);

            await _postRepository.CreateAsync(post);
            return new PublicJsonResult { result = true, id = post.Id, message = Messages.Post_Create_Success };
        }







        /// <summary>
        /// 
        /// </summary>
        public async Task<PublicJsonResult> UpdateAsync(PostInput input)
        {
            var post = await _postRepository.GetByIdAsync(input.Id);
            if (post == null)
            {
                throw new Exception("Post not found");
            }

            //بررسی یکتا بودن عنوان 
            var existPost = await _postRepository.GetByTitleAsync(input.Title.Trim());
            if (existPost != null && existPost.Id != input.Id)
                return new PublicJsonResult { result = false, message = Messages.Post_Title_Already_Exist };


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
            post.CategoryIds = input.CategoryIds;
            post.TagIds = await AddTagsToPostAsync(input.Tags);
            post.CategoryIds = AddTagsToPost(input.Categories);

            await _postRepository.UpdateAsync(post);
            return new PublicJsonResult { result = true, message = Messages.Post_Update_Success };


        }







        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<PostOutput> Search(int page, int recordsPerPage, string term, bool isTrashed, PublishStatus? publishStatus, SortOrder sortOrder, out int pageSize, out int TotalItemCount)
        {

            var posts = _postRepository.Search(page: page, recordsPerPage: recordsPerPage, term: term, isTrashed: isTrashed, publishStatus: publishStatus, sortOrder: sortOrder, pageSize: out pageSize, TotalItemCount: out TotalItemCount);

            return posts.Select(post =>
                                _mapperService.BindToOutputModel(

                                post: post,
                                tags: GetPostTaxonomies(post.TagIds),
                                categories: GetPostTaxonomies(post.CategoryIds),
                                user: _userRepository.Get(post.UserId)

                        )).ToList();
        }





        /// <summary>
        /// 
        /// </summary>
        public async Task<IEnumerable<PostOutput>> SearchAsync(int page, int recordsPerPage, string term, string taxonomyId, TaxonomyType? taxonomyType, PublishStatus? publishStatus, SortOrder sortOrder)
        {

            var posts = await _postRepository.SearchAsync(page: page, recordsPerPage: recordsPerPage, term: term, taxonomyId: taxonomyId, taxonomyType: taxonomyType, publishStatus: publishStatus, sortOrder: sortOrder);

            return posts.Select(post =>
                                _mapperService.BindToOutputModel(

                                post: post,
                                tags: GetPostTaxonomies(post.TagIds),
                                categories: GetPostTaxonomies(post.CategoryIds),
                                user: _userRepository.Get(post.UserId)

                        )).ToList();
        }








        /// <summary>
        /// 
        /// </summary>

        public async Task<PublicJsonResult> DeleteAsync(string id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                throw new Exception("Post not found");
            }

            if (post.IsTrashed)
            {
                await _postRepository.DeleteAsync(id);
            }
            else
            {
                post.IsTrashed = true;
                await _postRepository.UpdateAsync(post);
            }


            return new PublicJsonResult { result = true, message = Messages.Post_Delete_Success };
        }







        /// <summary>
        /// 
        /// </summary>

        public async Task<PublicJsonResult> RecycleAsync(string id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                throw new Exception("Post not found");
            }

            post.IsTrashed = false;
            await _postRepository.UpdateAsync(post);

            return new PublicJsonResult { result = true, message = Messages.Post_Recycle_Success };
        }




        #endregion


        #region Private Methods


        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<Taxonomy> GetPostTaxonomies(string[] taxIds)
        {
            if (taxIds == null)
                return new List<Taxonomy>();

            return _taxonomyRepository.GetListByIds(taxIds);

        }




        /// <summary>
        /// 
        /// </summary>
        private string[] GetPostTaxonomiesNameArray(string[] tagIds)
        {
            var taxs = GetPostTaxonomies(tagIds);
            return taxs.Select(t => t.Name).ToArray();
        }





        /// <summary>
        /// 
        /// </summary>
        private async Task<string[]> AddTagsToPostAsync(string tags)
        {
            if (tags == null)
                return new string[] { };


            var tagsArray = tags.Split(",");
            var existingTags = await _taxonomyRepository.GetAllAsync(TaxonomyType.Tag);
            List<string> outputTagIds = new List<string>();

            foreach (var tag in tagsArray)
            {
                var existingTag = existingTags.FirstOrDefault(t => t.Name == tag);
                if (existingTag != null)
                    outputTagIds.Add(existingTag.Id);
                else
                {
                    var newTag = new Taxonomy
                    {
                        Name = tag,
                        Type = TaxonomyType.Tag,
                        UrlTitle = tag.GenerateUrlTitle(),
                    };
                    await _taxonomyRepository.CreateAsync(newTag);

                    outputTagIds.Add(newTag.Id);
                }
            }

            return outputTagIds.ToArray();
        }





        /// <summary>
        /// 
        /// </summary>
        private string[] AddTagsToPost(SelectListItem[] categories)
        {
            return categories.Where(c => c.Selected).Select(c => c.Value).ToArray();


        }


        #endregion

    }
}
