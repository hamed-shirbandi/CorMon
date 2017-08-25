using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CorMon.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {

        public DashboardController()
        {

        }



        /// <summary>
        /// 
        /// </summary>
        [Route("admin/dashboard")]
        public IActionResult Index()
        {
            return View();
        }
    }
}