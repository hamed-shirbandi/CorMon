using CorMon.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CorMon.Application.Posts.Dto
{
   public class PostOutput
    {
        [Display(Name = "Post_Title", ResourceType = typeof(Metadata))]
        public string Title { get; set; }

        [Display(Name = "Post_Content", ResourceType = typeof(Metadata))]
        public string Content { get; set; }


        [Display(Name = "Post_Author", ResourceType = typeof(Metadata))]
        public string Author { get; set; }
    }
}
