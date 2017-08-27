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
    public class HomeController : BaseController
    {
        #region Fields

        private readonly IPostService _postService;

        #endregion


        #region Ctor

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }


        #endregion


        #region Methods




        /// <summary>
        /// 
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.SearchAsync(term: "", publishStatus: PublishStatus.Publish, sortOrder: SortOrder.Desc);

            return View(posts);
        }





        /// <summary>
        /// 
        /// </summary>
        public IActionResult SetLanguage(string lang = "en-US")
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(new CultureInfo(lang))), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return RedirectToAction("Index");
        }

        #endregion

    }
}
