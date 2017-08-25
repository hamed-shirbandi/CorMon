

using CorMon.Core.Domain;
using CorMon.Core.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorMon.Core.Data
{
    public interface IPostRepository
    {
        Task<Post> GetAsync(string id);
        Task<Post> GetAsync(string title,PostType postType);
        Task<IEnumerable<Post>> SearchAsync(string term);
        Task<Post> InsertAsync(Post post);
        Task InsertAsync(IEnumerable<Post> posts);
        Task<Post> UpdateAsync(Post post);
        Task UpdateAsync(IEnumerable<Post> posts);
    }
}
