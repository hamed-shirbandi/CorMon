using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using CorMon.Application.Posts.Dto;
using CorMon.Core.Domain;
using CorMon.Application.Taxonomies.Dto;
using CorMon.Application.Users.Dto;
using System.Collections.Generic;

namespace CorMon.Application.Mapper
{
    public class MapperService : IMapperService
    {

        #region Fields

        private readonly IConfiguration _configuration;


        #endregion

        #region Ctor

        public MapperService(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        #endregion

        #region Public Methods


        #region User



        /// <summary>
        /// 
        /// </summary>
        public UserInput BindToInputModel(User user)
        {
            return new UserInput
            {
                Id = user.Id.ToString(),
                DisplayName = user.DisplayName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                About = user.About,

            };
        }




        /// <summary>
        /// 
        /// </summary>
        public UserOutput BindToOutputModel(User user)
        {
            return new UserOutput
            {
                Id = user.Id.ToString(),
                DisplayName = user.DisplayName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                About = user.About,
                AvatarUrl = !string.IsNullOrEmpty(user.AvatarUrl) ? _configuration["ProjectUrl:Static"] + user.AvatarUrl : "",

            };
        }

        #endregion

        #region Role



        #endregion

        #region Post



        /// <summary>
        /// 
        /// </summary>
        public PostInput BindToInputModel(Post post, string[] tagsPrefill)
        {
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
                TagsPrefill = tagsPrefill,
                CategoryIds = post.CategoryIds,
                TagIds = post.TagIds,
                IsTrashed = post.IsTrashed,
            };
        }





        /// <summary>
        /// 
        /// </summary>
        public PostOutput BindToOutputModel(Post post, User user, IEnumerable<Taxonomy> tags, IEnumerable<Taxonomy> categories)
        {
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
                Categoories = categories.Select(taxonomy => BindToOutputModel(taxonomy)),
                Tags = tags.Select(taxonomy => BindToOutputModel(taxonomy)),
                IsTrashed = post.IsTrashed,
            };
        }


        /// <summary>
        /// 
        /// </summary>
        public Post BindToDomainModel(PostInput input, string[] categoryIds, string[] tagIds)
        {
            return new Post
            {
                Title = input.Title,
                Author = input.Author,
                Content = input.Content,
                PostLevel = input.PostLevel,
                MetaDescription = input.MetaDescription,
                MetaKeyWords = input.MetaKeyWords,
                PublishDateTime = input.PublishDateTime,
                PublishStatus = input.PublishStatus,
                MetaRobots = input.MetaRobots,
                UrlTitle = input.UrlTitle,
                UserId = input.UserId,
                CategoryIds = categoryIds,
                TagIds = tagIds,

            };
        }




        #endregion

        #region Taxonomy



        /// <summary>
        /// 
        /// </summary>
        public Taxonomy BindToDomainModel(TaxonomyInput input)
        {
            return new Taxonomy
            {
                Name = input.Name,
                Description = input.Description,
                PostCount = input.PostCount,
                Type = input.Type,
                UrlTitle = input.UrlTitle,
            };
        }




        /// <summary>
        /// 
        /// </summary>
        public TaxonomyOutput BindToOutputModel(Taxonomy taxonomy)
        {
            return new TaxonomyOutput
            {
                Id = taxonomy.Id,
                Name = taxonomy.Name,
                Description = taxonomy.Description,
                PostCount = taxonomy.PostCount,
                Type = taxonomy.Type,
                UrlTitle = taxonomy.UrlTitle,
            };
        }




        /// <summary>
        /// 
        /// </summary>
        public TaxonomyInput BindToInputModel(Taxonomy taxonomy)
        {
            return new TaxonomyInput
            {
                Id = taxonomy.Id,
                Name = taxonomy.Name,
                Description = taxonomy.Description,
                PostCount = taxonomy.PostCount,
                Type = taxonomy.Type,
                UrlTitle = taxonomy.UrlTitle,

            };
        }



        #endregion


        #endregion

        #region Private Methods

        #endregion
    }
}
