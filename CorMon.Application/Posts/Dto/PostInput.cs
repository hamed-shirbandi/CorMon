using CorMon.Core.Enums;
using CorMon.Core.Helpers;
using CorMon.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CorMon.Application.Posts.Dto
{
    public class PostInput
    {

        public string ActionName { get; set; }
        public string Id { get; set; }

        [Display(Name = "Post_Author", ResourceType = typeof(Metadata))]
        public string Author { get; set; }


        [Display(Name = "Post_Title", ResourceType = typeof(Metadata))]
        [Required(ErrorMessageResourceName = "Post_Title_Required", ErrorMessageResourceType = typeof(Metadata))]
        public string Title { get; set; }


        [Display(Name = "Post_Content", ResourceType = typeof(Metadata))]
        [Required(ErrorMessageResourceName = "Post_Content_Required", ErrorMessageResourceType = typeof(Metadata))]
        public string Content { get; set; }


        [Display(Name = "Post_PostLevel", ResourceType = typeof(Metadata))]
        [Required(ErrorMessageResourceName = "Post_PostLevel_Required", ErrorMessageResourceType = typeof(Metadata))]
        public PostLevel PostLevel { get; set; }


        [Display(Name = "Post_UrlTitle", ResourceType = typeof(Metadata))]
        public string UrlTitle { get; set; }


        [Display(Name = "Post_MetaDescription", ResourceType = typeof(Metadata))]
        public string MetaDescription { get; set; }



        [Display(Name = "Post_MetaKeyWords", ResourceType = typeof(Metadata))]
        public string MetaKeyWords { get; set; }


        [Display(Name = "Post_RobotsState", ResourceType = typeof(Metadata))]
        [Required(ErrorMessageResourceName = "Post_RobotsState_Required", ErrorMessageResourceType = typeof(Metadata))]
        public RobotsState MetaRobots { get; set; }


        [Display(Name = "Post_PublishStatus", ResourceType = typeof(Metadata))]
        [Required(ErrorMessageResourceName = "Post_PublishStatus_Required", ErrorMessageResourceType = typeof(Metadata))]
        public PublishStatus PublishStatus { get; set; }



        [Display(Name = "Post_PublishDateTime", ResourceType = typeof(Metadata))]
        [Required(ErrorMessageResourceName = "Post_PublishDateTime_Required", ErrorMessageResourceType = typeof(Metadata))]
        public DateTime PublishDateTime { get; set; }

        [Display(Name = "Post_CreateDateTime", ResourceType = typeof(Metadata))]
        public DateTime CreateDateTime { get; set; }


        [Display(Name = "Post_ModifiedDateTime", ResourceType = typeof(Metadata))]
        public DateTime ModifiedDateTime { get; set; }




        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string[] TagIds { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public string[] CategoryIds { get; set; }




        /// <summary>
        ///
        /// </summary>
        //[Display(Name = "PostTags", ResourceType = typeof(Metadata))]
        public string[] TagsPrefill { get; set; }


        /// <summary>
        ///
        /// </summary>
        public string Tags { get; set; }



        /// <summary>
        /// 
        /// </summary>
        ///  //[Display(Name = "Post_Categories", ResourceType = typeof(Metadata))]
        public SelectListItem[] Categories { get; set; }



    }
}
