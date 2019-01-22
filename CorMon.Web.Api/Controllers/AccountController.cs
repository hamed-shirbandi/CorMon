using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using CorMon.Core.Domain;
using CorMon.Application.Users;
using CorMon.Web.Api.Services.Jwt;
using CorMon.Core.JsonModels;
using CorMon.Resource;
using CorMon.Web.Api.Models;

namespace CorMon.Web.Api.Controllers
{
    public class AccountController : BaseController
    {
        #region Fields

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;



        #endregion

        #region Ctor

        public AccountController(IUserService userService, IJwtService jwtService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _jwtService = jwtService;
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        #endregion

        #region Public Methods



        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [Route("account/login")]
        public async Task<PublicJsonResult> Login([FromBody] LoginViewModel model)
        {

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded)
                return new PublicJsonResult { Result = false, Message = Messages.User_Login_Failed };

            var user = await _userService.GetByEmailAsync(model.Email);


            return new PublicJsonResult { Result = true, Token = _jwtService.GenerateJwtToken(user) };
        }


        #endregion


        #region  Private Methods






        #endregion

    }


}