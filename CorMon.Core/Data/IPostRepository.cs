

using CorMon.Core.Domain;
using CorMon.Core.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorMon.Core.Data
{
    public interface IPostRepository
    {
        Task<Post> GetByIdAsync(string id);
        Task<Post> GetByTitleAsync(string title);
        IEnumerable<Post> Search(int page, int recordsPerPage, string term, bool isTrashed, PublishStatus? publishStatus, SortOrder sortOrder, out int pageSize, out int TotalItemCount);
       Task< IEnumerable<Post>> SearchAsync(int page, int recordsPerPage, string term, string taxonomyId, TaxonomyType? taxonomyType, PublishStatus? publishStatus, SortOrder sortOrder);
        Task CreateAsync(Post post);
        Task CreateAsync(IEnumerable<Post> posts);
        Task UpdateAsync(Post post);
        Task UpdateAsync(IEnumerable<Post> posts);
        Task DeleteAsync( string id);

    }
}
