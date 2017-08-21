using System;
using System.Collections.Generic;
using System.Text;

namespace CorMon.Core.Domain
{
    public class Post: BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
    }
}
