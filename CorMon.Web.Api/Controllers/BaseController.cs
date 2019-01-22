using CorMon.Core.Extensions;
using CorMon.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace CorMon.Web.Api.Controllers
{
    public class BaseController : Controller
    {
        #region Fields

     
        #endregion

        #region Ctor

        public BaseController()
        {
           

        }






        #endregion

        #region Public Methods






        #endregion

        #region  protected Methods



        /// <summary>
        /// 
        /// </summary>
        protected string GetCurrentUserName()
        {
            if (this.User == null)
                return "";
            ClaimsIdentity claimsIdentity = this.User.Identity as ClaimsIdentity;
            if (claimsIdentity.Claims.Count() == 0)
                return "";
            var userName = claimsIdentity.Claims.FirstOrDefault(c => c.Type == nameof(JwtBaseModel.UserName).ToLowerFirst()).Value;
            return userName;

        }



        /// <summary>
        /// 
        /// </summary>
        protected long GetCurrentUserId()
        {
            if (this.User == null)
                return 0;

            ClaimsIdentity claimsIdentity = this.User.Identity as ClaimsIdentity;
            if (claimsIdentity.Claims.Count() == 0)
                return 0;
            var userId = claimsIdentity.Claims.FirstOrDefault(c => c.Type == nameof(JwtBaseModel.Id).ToLowerFirst()).Value;
            return long.Parse(userId);

        }





        #endregion

    }
}