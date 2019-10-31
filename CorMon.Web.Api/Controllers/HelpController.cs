using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CorMon.Web.Api.Controllers
{
  
    public class HelpController : BaseController
    {
        #region Fields

        private readonly IConfiguration _configuration;

        #endregion

        #region Ctor

        public HelpController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Public Methods



        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public string Get_Api_Documentation_Url()
        {
            return $"CorMon Api Documentation ==> {_configuration["ProjectUrl:Api"]}/swagger";
        }


        




        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public string Check_Api_Version()
        {
            return "1.0";
        }





        #endregion

        #region  Private Methods




        #endregion

    }
}