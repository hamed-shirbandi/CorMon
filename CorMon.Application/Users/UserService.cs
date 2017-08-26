using CorMon.Application.Users.Dto;
using CorMon.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CorMon.Application.Users
{
  public  class UserService: IUserService
    {
        #region Fields

        private readonly IUserRepository _userRepository;


        #endregion

        #region Ctor

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        #endregion

        #region Methods








        /// <summary>
        /// 
        /// </summary>
        public async Task<UserOutput> Get(string id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            return new UserOutput
            {
                DisplayName = user.DisplayName,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.Phone,
                About = user.About,

            };
        }







        #endregion
    }
}
