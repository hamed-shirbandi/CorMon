

using CorMon.Core.Domain;
using CorMon.Core.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorMon.Core.Data
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string id);
        User Get(string id);
        Task UpdateAsync(User user);
    }
}
