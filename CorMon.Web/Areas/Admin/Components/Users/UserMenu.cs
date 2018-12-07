using Microsoft.AspNetCore.Mvc;
using CorMon.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorMon.Web.Areas.Admin.Components.Users
{
    public class UserMenu: ViewComponent
    {

        #region Fileds

        private readonly IUserService _userService;


        #endregion

        #region Ctor

        public UserMenu(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Public Methods



        /// <summary>
        /// 
        /// </summary>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // get sample user.
            //This is because we don't have user management right now
            var currentUser = await _userService.GetSampleUserAsync();

            return View(currentUser);
        }

        #endregion


    }
}
