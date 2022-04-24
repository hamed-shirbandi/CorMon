using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using CorMon.Application.Posts;
using CorMon.Core.Enums;

namespace CorMon.Web.Controllers
{
    public class ErrorController : BaseController
    {
        #region Fields

        private readonly IPostService _postService;

        #endregion


        #region Ctor

        public ErrorController(IPostService postService)
        {
            _postService = postService;
        }


        #endregion


        #region Public Methods




        /// <summary>
        /// 
        /// </summary>
        public async Task<IActionResult> Unknown()
        {
            return View("Error");
        }



        
        #endregion

    }
}
