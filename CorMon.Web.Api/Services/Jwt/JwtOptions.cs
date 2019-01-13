using System;
using System.Collections.Generic;
using System.Text;

namespace CorMon.Web.Api.Services.Jwt
{
    public class JwtOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpireDays { get; set; }
    }
}
