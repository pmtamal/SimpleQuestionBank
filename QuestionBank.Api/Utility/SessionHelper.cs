using QuestionBank.Model.Api;
using System.Security.Claims;

namespace QuestionBank.Api.Utility
{
    public class SessionHelper
    {
        public static UserSessionModel GetUserSessionModelFromClaim(IEnumerable<Claim> claims)
        {
            var userId = int.Parse(claims.First(x => x.Type == "Id").Value);
            var userEmail = claims.First(x => x.Type == "Email").Value;

            return new UserSessionModel
            {
                UserEmail = userEmail,
                UserId = userId

            };

        }
    }
}
