using Newtonsoft.Json;
using Smag.Core.Common.Authentication;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Smag.Core.Common.Authentication
{
    /// <summary>
    /// Store information about the user.
    /// </summary>
    public class AuthenticationContext
    {
        private const string ApplicationIdKey = "https://smag.authentication.operational.com/application_id";
        private const string IdentitiesKey = "https://smag.authentication.operational.com/identities";
        private const string UserIdKey = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        private const string TokenKey = "Token";
        private const string AuthIdKey = "sub";

        #region Properties

        /// <summary>
        /// Application Id
        /// </summary>
        public string ApplicationId { get; set; }

        /// <summary>
        /// Smag user Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Auth0 Id of current user
        /// </summary>
        public string AuthId { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Refresh token
        /// </summary>
        public string RefreshToken { get; set; }

        public Identity Identity => Identities?.FirstOrDefault();

        /// <summary>
        /// Legacy subscriptions of all applications
        /// </summary>
        public Identity[] Identities { get; set; }

        /// <summary>
        /// Azure VaultKey for application, in order to get info (legacy database connection string)
        /// </summary>
        public string VaultKey { get; set; }

        #endregion Properties

        #region Constructors

        public AuthenticationContext()
        {
        }

        public AuthenticationContext(Claim[] claims)
        {
            if (claims == null) return;

            ApplicationId = claims.FirstOrDefault(x => x.Type == ApplicationIdKey)?.Value;

            var identitiesClaimValues = claims.Where(x => x.Type == IdentitiesKey).ToArray();

            Identities = identitiesClaimValues.Select(x => JsonConvert.DeserializeObject<Identity>(x.Value)).ToArray();
            UserId = claims.FirstOrDefault(x => x.Type == UserIdKey)?.Value;

            var token = claims.FirstOrDefault(x => x.Type == TokenKey)?.Value;
            Token = token;

            AuthId = string.IsNullOrEmpty(token)
                ? string.Empty
                : JwtTokenHelper.GetClaimInfo(token, AuthIdKey);
        }

        public AuthenticationContext(ClaimsIdentity identity) : this(identity?.Claims.ToArray())
        {
        }

        public AuthenticationContext(IIdentity identity) : this((identity as ClaimsIdentity)?.Claims.ToArray())
        {
        }

        #endregion Constructors
    }
}