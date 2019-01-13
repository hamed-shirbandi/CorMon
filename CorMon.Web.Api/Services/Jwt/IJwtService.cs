
using CorMon.Core.Helpers;

namespace CorMon.Web.Api.Services.Jwt
{
    public interface IJwtService
    {
        string GenerateJwtToken<T>(T user) where T:JwtBaseModel;
    }
}
