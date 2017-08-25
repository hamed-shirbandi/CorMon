using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CorMon.Web.Models;
using CorMon.Application.Posts;
using CorMon.Application.Posts.Dto;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace CorMon.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        #endregion

        #region Ctor

        public HomeController()
        {
          
        }

        #endregion

        #region Methods


        public async Task<IActionResult> Index()
        {
    
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
