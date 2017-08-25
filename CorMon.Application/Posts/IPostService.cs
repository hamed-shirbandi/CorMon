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
        Task<PostInput> GetToUpdate(string id);
        Task<PostOutput> Get(string id);
        Task<PostJsonResult> InsertAsync(PostInput input);
        Task<IEnumerable<PostOutput>> SearchAsync(string  term);
    }
}
