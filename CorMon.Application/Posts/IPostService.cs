using CorMon.Application.Posts.Dto;
using CorMon.Core.Enums;
using CorMon.Core.JsonModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CorMon.Application.Posts
{
   public interface IPostService
    {
        Task<PostInput> GetToUpdateAsync(string id);
        Task<PostOutput> GetAsync(string id);
        Task<PostJsonResult> InsertAsync(PostInput input);
        Task<IEnumerable<PostOutput>> SearchAsync(string  term,PublishStatus? publishStatus, SortOrder sortOrder);
    }
}
