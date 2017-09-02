using CorMon.Application.Users.Dto;
using CorMon.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CorMon.Core.JsonModels;
using CorMon.Resource;

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

        #region Public Methods








        /// <summary>
        /// 
        /// </summary>
        public async Task<UserOutput> GetAsync(string id)
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

        public async Task<PublicJsonResult> UpdateAsync(UserInput input)
        {
            var user = await _userRepository.GetAsync(input.Id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.DisplayName = input.DisplayName;
            user.About = input.About;
            user.Email = input.Email;
            user.Phone = input.Phone;
            user.UserName = input.UserName;
            await _userRepository.UpdateAsync(user);

            return new PublicJsonResult { result=true,message=Messages.User_Update_Success};
        }







        #endregion
    }
}
