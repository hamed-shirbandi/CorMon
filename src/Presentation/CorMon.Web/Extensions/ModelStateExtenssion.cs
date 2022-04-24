using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorMon.Web.Extensions
{
    public static class ModelStateExtenssion
    {


        /// <summary>
        /// دریافت کلیه خطاهای مدل دریافتی و برگشت آن به صورت رشته
        /// </summary>
        public static string GetErrors(this ModelStateDictionary modelState)
        {
            var getError = string.Empty;
            foreach (var value in modelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    getError += error.ErrorMessage + "<br />";
                }
            }
            return getError;
        }

    }
}
