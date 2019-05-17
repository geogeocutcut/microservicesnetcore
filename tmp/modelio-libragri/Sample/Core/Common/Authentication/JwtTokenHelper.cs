using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Core.Common.Authentication
{
    public class JwtTokenHelper
    {
        /// <summary>
        /// single value property
        /// </summary>
        public static string GetClaimInfo(string token, string claimKey)
        {
            var tokenS = (new JwtSecurityTokenHandler()).ReadToken(token) as JwtSecurityToken;
            return tokenS?.Claims.FirstOrDefault(claim => claim.Type.Equals(claimKey))?.Value;
        }

        /// <summary>
        /// collection value property
        /// </summary>
        public static string[] GetClaimInfoArray(string token, string claimKey)
        {
            var tokenS = (new JwtSecurityTokenHandler()).ReadToken(token) as JwtSecurityToken;
            return tokenS?.Claims.Where(claim => claim.Type.Equals(claimKey))?.Select(x => x.Value).ToArray();
        }
    }
}