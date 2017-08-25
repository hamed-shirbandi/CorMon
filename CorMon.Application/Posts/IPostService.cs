using CorMon.Application.Posts.Dto;
using CorMon.Core.JsonModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CorMon.Application.Posts
{
   public interface IPostService
    {
        Task<PostJsonResult> InsertAsync(PostInput input);
        Task<IEnumerable<PostOutput>> SearchAsync(string  term);
    }
}
