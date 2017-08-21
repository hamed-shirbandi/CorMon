using CorMon.Application.Posts.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CorMon.Application.Posts
{
   public interface IPostService
    {
        Task InsertAsync(PostInput input);
        Task<IEnumerable<PostOutput>> SearchAsync(string  term);
    }
}
