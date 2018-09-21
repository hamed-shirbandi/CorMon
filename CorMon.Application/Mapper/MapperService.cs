using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using CorMon.Application.Posts.Dto;
using CorMon.Core.Domain;
using CorMon.Application.Taxonomies.Dto;
using CorMon.Application.Users.Dto;

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


        

        /// <summary>
        /// 
        /// </summary>
        public PostInput BindToInputModel(Post Post)
        {
            return new PostInput
            {
               
            };
        }





        /// <summary>
        /// 
        /// </summary>
        public PostOutput BindToOutputModel(Post Post)
        {
            return new PostOutput
            {
               
            };
        }


        

        /// <summary>
        /// 
        /// </summary>
        public TaxonomyOutput BindToOutputModel(Taxonomy Taxonomy)
        {
            return new TaxonomyOutput
            {
               

            };
        }




        /// <summary>
        /// 
        /// </summary>
        public TaxonomyInput BindToInputModel(Taxonomy Taxonomy)
        {
            return new TaxonomyInput
            {
               

            };
        }

        

        /// <summary>
        /// 
        /// </summary>
        public UserInput BindToInputModel(User User)
        {
            return new UserInput
            {
              

            };
        }


        

        /// <summary>
        /// 
        /// </summary>
        public UserOutput BindToOutputModel(User User)
        {
            return new UserOutput
            {
               

            };
        }

        
        

        #endregion

        #region Private Methods

        #endregion
    }
}
