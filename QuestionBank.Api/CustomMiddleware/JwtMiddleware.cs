using Microsoft.Extensions.Options;
using QuestionBank.Api.Utility;
using QuestionBank.Common;
using QuestionBank.Utility;

namespace QuestionBank.Api.CustomMiddleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachUserToContext(context, token);

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                //var tokenHandler = new JwtSecurityTokenHandler();
                //var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecretKey);
                //tokenHandler.ValidateToken(token, new TokenValidationParameters
                //{
                //    ValidateIssuerSigningKey = true,
                //    IssuerSigningKey = new SymmetricSecurityKey(key),
                //    ValidateIssuer = false,
                //    ValidateAudience = false,

                //    // set clock skew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                //    ClockSkew = TimeSpan.Zero
                //}, out SecurityToken validatedToken);

                //var jwtToken = (JwtSecurityToken)validatedToken;

                var claims = await JWTHelper.GetClaimsFromToken(_appSettings.JwtSecretKey, token);
                

                //Attach user to context on successful JWT validation
                context.Items["User"] = SessionHelper.GetUserSessionModelFromClaim(claims);
            }
            catch
            {
                //Do nothing if JWT validation fails
                // user is not attached to context so the request won't have access to secure routes
            }
        }
    }
}