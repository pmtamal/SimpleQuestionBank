using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuestionBank.Utility
{
    public class JWTHelper
    {
        public static async Task<Token> GenerateJwtToken(string securityKey, Dictionary<string, string> claims, DateTime expiredOnUtc)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {

                var key = Encoding.ASCII.GetBytes(securityKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims.Select(kvp => new Claim(kvp.Key, kvp.Value))),
                    Expires = expiredOnUtc,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                return tokenHandler.CreateToken(tokenDescriptor);
            });

            var refreshToken = GenerateRefreshToken();

            return new Token { AccessToken = tokenHandler.WriteToken(token), RefreshToken = refreshToken };

        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async static Task<IEnumerable<Claim>> GetClaimsFromToken(string securityKey, string token,bool validateLifeTime=true)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(securityKey);
            var claims = await Task.Run(() =>
            {
                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime=validateLifeTime,

                        // set clock skew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);





                    var jwtToken = (JwtSecurityToken)validatedToken;

                    if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    {
                        throw new SecurityTokenException("Invalid token");
                    }

                    return jwtToken.Claims;
                }
                catch (Exception)
                {

                    return null;
                }
            });

            return claims;
            //var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);
            //var userEmail = jwtToken.Claims.First(x => x.Type == "Email").Value;

            ////Attach user to context on successful JWT validation
            //context.Items["User"] = new UserSessionModel()
            //{
            //    UserEmail = userEmail,
            //    UserId = userId
            //};

        }
    }


}
