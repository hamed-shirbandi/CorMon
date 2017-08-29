

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
        Task<IEnumerable<Post>> SearchAsync(string term, PublishStatus? publishStatus, SortOrder sortOrder);
        Task CreateAsync(Post post);
        Task CreateAsync(IEnumerable<Post> posts);
        Task UpdateAsync(Post post);
        Task UpdateAsync(IEnumerable<Post> posts);
    }
}
