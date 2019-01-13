using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Linq;
using System.Reflection;
using CorMon.Core.Helpers;
using CorMon.Core.Extensions;

namespace CorMon.Web.Api.Services.Jwt
{
    public class JwtService : IJwtService
    {
        #region Fields

        private readonly JwtOptions _options;

        #endregion

        #region Ctor

        public JwtService(IOptions<JwtOptions> options)
        {
            _options = options != null ? options.Value : throw new ArgumentNullException(nameof(options));

        }

        #endregion

        #region Public Methods


        /// <summary>
        /// 
        /// </summary>
        public  string GenerateJwtToken<T>(T user) where T:JwtBaseModel
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),//username
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId)
            };


            var properties = new List<PropertyInfo>(user.GetType().GetProperties());

            foreach (PropertyInfo property in properties)
            {
                object propValue = property.GetValue(user, property.GetIndexParameters());

                string name = property.Name.ToString().ToLowerFirst();
                string value = propValue != null ? propValue.ToString() : "";
                claims.Add(new Claim(name, value));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_options.ExpireDays));

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }




        #endregion

        #region Private Methods



        #endregion




    }
}
