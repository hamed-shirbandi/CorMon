using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CorMon.Web.Models;
using CorMon.Application.Posts;
using CorMon.Application.Posts.Dto;

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

        #endregion

    }
}
