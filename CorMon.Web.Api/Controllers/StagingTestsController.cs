using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CorMon.Web.Api.Controllers
{
  
    public class StagingTestsController : BaseController
    {
        #region Fields

        private readonly IConfiguration _configuration;

        #endregion

        #region Ctor

        public StagingTestsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Public Methods



        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public string Get_Api_Documentation()
        {
            return $"CorMon Api Documentation ==> {_configuration["ProjectUrl:Api"]}/swagger";
        }




        /// <summary>
        /// use in ui tests
        /// </summary>
        [HttpGet]
        public string Check_App_Initialization()
        {
            return "OK" ;
        }



        



        #endregion

        #region  Private Methods




        #endregion

    }
}