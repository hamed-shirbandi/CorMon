using CorMon.Core.Enums;
using CorMon.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CorMon.Application.Posts.Dto
{
   public class PostOutput
    {
       
        [Display(Name = "Post_Author", ResourceType = typeof(Metadata))]
        public string Author { get; set; }
        public string UserId { get; set; }


        [Display(Name = "Post_Title", ResourceType = typeof(Metadata))]
        public string Title { get; set; }


        [Display(Name = "Post_Content", ResourceType = typeof(Metadata))]
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
        public RobotsState RobotsState { get; set; }

        [Display(Name = "Post_PublishStatus", ResourceType = typeof(Metadata))]
        public PublishStatus PublishStatus { get; set; }

        [Display(Name = "Post_PublishDateTime", ResourceType = typeof(Metadata))]
        public DateTime PublishDateTime { get; set; }

        public string ThumbnailUrl { get; set; }
        public string ThumbnailTileUrl { get; set; }
        public bool IsDeleted { get; set; }
        public PostType PostType { get; set; }
    }
}
