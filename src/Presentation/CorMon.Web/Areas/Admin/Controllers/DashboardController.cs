﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorMon.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class DashboardController : Controller
    {

        public DashboardController()
        {

        }



        /// <summary>
        /// 
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
    }
}