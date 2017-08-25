using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace CorMon.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region Fields


        #endregion

        #region Ctor

        public HomeController()
        {
          
        }

        #endregion

        #region Methods




        /// <summary>
        /// 
        /// </summary>
        public async Task<IActionResult> Index()
        {
    
            return View();
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
