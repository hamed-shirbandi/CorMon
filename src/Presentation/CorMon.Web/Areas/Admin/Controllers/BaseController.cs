﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CorMon.Web.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {

        }




        /// <summary>
        /// 
        /// </summary>
        protected string GetCurrentUserName()
        {
            if (this.User != null)
            {
                return this.User.Identity.Name;
            }

            return "";
        }



        /// <summary>
        /// 
        /// </summary>
        protected string GetCurrentUserId()
        {
            if (this.User != null)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(userId))
                {
                    return userId;
                }
            }

            return "";

        }


    }
}