using CorMon.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CorMon.Application.Posts.Dto
{
   public class PostInput
    {
        [Display(Name = "Post_Title", ResourceType =typeof(Metadata))]
        [Required(ErrorMessageResourceName = "Post_Title_Required", ErrorMessageResourceType =typeof(Metadata))]
        public string Title { get; set; }


        [Display(Name = "Post_Content", ResourceType = typeof(Metadata))]
        [Required(ErrorMessageResourceName = "Post_Content_Required", ErrorMessageResourceType = typeof(Metadata))]
        public string Content { get; set; }
    }
}
