using CorMon.Application.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CorMon.Application.Users
{
    public interface IUserService
    {
        Task<UserOutput> Get(string id);
    }
}
