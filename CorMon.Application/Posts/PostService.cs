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
                TagsPrefill = GetPostTaxonomiesNameArray(post.TagIds),
                CategoryIds = post.CategoryIds,
                TagIds = post.TagIds,

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
            if (user == null)
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
                Categoories = GetPostTaxonomies(post.CategoryIds),
                Tags = GetPostTaxonomies(post.CategoryIds),
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
                CategoryIds = AddTagsToPost(input.Categories),
                TagIds = await AddTagsToPostAsync(input.Tags),

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
            post.CategoryIds = input.CategoryIds;
            post.TagIds = await AddTagsToPostAsync(input.Tags);
            post.CategoryIds = AddTagsToPost(input.Categories);
           
            await _postRepository.UpdateAsync(post);
            return new PostJsonResult { result = true, message = Messages.Post_Update_Success };


        }







        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<PostOutput> Search(int page, int recordsPerPage, string term, PublishStatus? publishStatus, SortOrder sortOrder, out int pageSize, out int TotalItemCount)
        {
          
            var posts = _postRepository.Search(page:page,recordsPerPage:recordsPerPage,term:term, publishStatus: publishStatus, sortOrder: sortOrder,pageSize:out pageSize,TotalItemCount: out TotalItemCount);
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
                Categoories = GetPostTaxonomies(post.CategoryIds),
                Tags = GetPostTaxonomies(post.CategoryIds),

            }).ToList();
        }





        /// <summary>
        /// 
        /// </summary>
        public async Task<IEnumerable<PostOutput>>  SearchAsync(int page, int recordsPerPage, string term, PublishStatus? publishStatus, SortOrder sortOrder)
        {

            var posts =await _postRepository.SearchAsync(page: page, recordsPerPage: recordsPerPage, term: term, publishStatus: publishStatus, sortOrder: sortOrder);
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
                Categoories = GetPostTaxonomies(post.CategoryIds),
                Tags = GetPostTaxonomies(post.CategoryIds),

            }).ToList();
        }







        #endregion


        #region Private Methods


        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<TaxonomyOutput> GetPostTaxonomies(string[] taxIds)
        {
            if (taxIds==null)
                return new List<TaxonomyOutput>();

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
            if (tags==null)
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
        private  string[] AddTagsToPost(SelectListItem[] categories)
        {
           return categories.Where(c=>c.Selected).Select(c=>c.Value).ToArray();
         
            
        }


        #endregion

    }
}
