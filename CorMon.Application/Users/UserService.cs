using CorMon.Application.Users.Dto;
using CorMon.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CorMon.Core.JsonModels;
using CorMon.Resource;
using CorMon.Application.Mapper;

namespace CorMon.Application.Users
{
  public  class UserService: IUserService
    {
        #region Fields

        private readonly IUserRepository _userRepository;
        private readonly IMapperService _mapperService;


        #endregion

        #region Ctor

        public UserService(IUserRepository userRepository, IMapperService mapperService)
        {
            _userRepository = userRepository;
            _mapperService = mapperService;

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

            return _mapperService.BindToOutputModel(user);
        }






        /// <summary>
        /// 
        /// </summary>
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
