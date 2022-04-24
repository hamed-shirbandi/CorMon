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
        Task<PublicJsonResult> CreateAsync(PostInput input);
        Task<PublicJsonResult> UpdateAsync(PostInput input);
        IEnumerable<PostOutput> Search(int page, int recordsPerPage, string term, bool isTrashed, PublishStatus? publishStatus, SortOrder sortOrder, out int pageSize, out int TotalItemCount);
        Task<IEnumerable<PostOutput>> SearchAsync(int page, int recordsPerPage, string term, string taxonomyId , TaxonomyType? taxonomyType , PublishStatus? publishStatus, SortOrder sortOrder);
        Task<PublicJsonResult> DeleteAsync(string id);
        Task<PublicJsonResult> RecycleAsync(string id);
        PostOutput Get(string id);
    }
}
