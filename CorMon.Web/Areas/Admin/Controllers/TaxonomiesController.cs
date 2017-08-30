using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using CorMon.Application.Posts;
using CorMon.Core.Enums;
using CorMon.Application.Taxonomies;


namespace CorMon.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaxonomiesController : BaseController
    {
        #region Fields

        private readonly ITaxonomyService _taxonomyService;


        #endregion

        #region Ctor

        public TaxonomiesController(ITaxonomyService taxonomyService)
        {
            _taxonomyService = taxonomyService;
        }

        #endregion



        #region Public Methods




        /// <summary>
        /// 
        /// </summary>
        public async Task<IActionResult> Index(TaxonomyType type = TaxonomyType.Category)
        {
            var posts = await _taxonomyService.SearchAsync(term: "", type: type, sortOrder: SortOrder.Desc);

            return View(posts);
        }





        /// <summary>
        /// دریافت نام برچسب ها برای پلاگین اتوکامپلیت
        /// </summary>
        [HttpGet]
        //[AjaxOnly]
        public async Task<JsonResult> GetTags(string term)
        {
            var tags = await _taxonomyService.SearchAsync(term: term, type: TaxonomyType.Tag, sortOrder: SortOrder.Desc);

            return Json(tags);
        }


        #endregion

    }
}
