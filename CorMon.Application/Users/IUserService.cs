using CorMon.Application.Users.Dto;
using CorMon.Core.JsonModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CorMon.Application.Users
{
    public interface IUserService
    {
        Task<UserOutput> GetAsync(string id);
        Task<PublicJsonResult> UpdateAsync(UserInput input);
    }
}
