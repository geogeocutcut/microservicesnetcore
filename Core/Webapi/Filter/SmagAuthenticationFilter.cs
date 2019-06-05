using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Core.Webapi.Filter
{
    /// <summary>
    /// how to use :
    /// In Startup.cs :
    /// public void ConfigureServices(IServiceCollection services)
    /// {
    ///     ...
    ///     services.AddMvc(op => op.Filters.Add<SmagAuthenticationFilter>());
    /// }
    /// </summary>
    public class SmagAuthenticationFilter : IAsyncActionFilter
    {
        private static readonly string[] AllowedBearerString = { "bearer", "Bearer" };
        private static byte[] _issuerSigningKey;
        private static string _smagAuthHeaderKey;
        private static string _clientId;
        private static string _issuer;
        private static IHttpContextAccessor _httpContextAccessor;
        private static string _environment;

        //read the certification file once for all
        public SmagAuthenticationFilter(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _smagAuthHeaderKey = configuration["Auth0:SmagAuthorizationHeaderKey"];
            _clientId = configuration["Auth0:ClientId"];
            _issuer = configuration["Auth0:Issuer"];
            _environment = configuration["Auth0:Environment"];
            InitIssuerSigningKey();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!ShouldCheckAuthentication(context))
                AuthenticationCheck(context?.HttpContext?.Request?.Headers);

            await next();
        }

        //try to find AllowAnonymousAttribute on controller level or on controller action lever
        private static bool ShouldCheckAuthentication(ActionExecutingContext context)
        {
            return context.Controller.GetType().GetCustomAttribute<AllowAnonymousAttribute>() != null
                   || context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor && controllerActionDescriptor.MethodInfo.GetCustomAttribute<AllowAnonymousAttribute>() != null;
        }

        private static void AuthenticationCheck(IHeaderDictionary headers)
        {
            if (headers == null)
                throw new AuthenticationException("Missing Json Web Token");

            var headerPair = headers.FirstOrDefault(o => o.Key == _smagAuthHeaderKey);

            if (headerPair.Key == null)
                throw new AuthenticationException($"No Smag Authorization header was present. Expecting {_smagAuthHeaderKey}: Bearer xxxxxx");

            var header = headerPair.Value.FirstOrDefault();

            if (header == null)
                throw new AuthenticationException($"No Smag Authorization header was present. Expecting this header key : {_smagAuthHeaderKey}");

            if (!AuthenticationHeaderValue.TryParse(header, out var authorization)
                || authorization == null
                || (!AllowedBearerString.Contains(authorization.Scheme)))
                //todo
                throw new AuthenticationException($"The Smag Authorization header is in the wrong format. Expecting SmagAuthorization: Bearer {{JWT}}");

            if (string.IsNullOrEmpty(authorization.Parameter))
                throw new AuthenticationException("Missing Json Web Token");

            var jwt = authorization.Parameter.Replace("bearer ", "");

            if (string.IsNullOrEmpty(jwt))
                throw new AuthenticationException("Missing credentials");

            ClaimsPrincipal claimsPrincipal;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = _clientId,
                    ValidIssuer = _issuer,
                    IssuerSigningKey = new X509SecurityKey(new X509Certificate2(_issuerSigningKey))
                };

                claimsPrincipal = tokenHandler.ValidateToken(jwt, validationParameters, out var securityToken);
            }
            catch (SecurityTokenValidationException ex)
            {
                throw new AuthenticationException("Invalid Json Web Token");
            }
            catch (System.Exception ex)
            {
                throw new AuthenticationException($"Authentication Error : An error occured while reading the Json Web Token {ex.Message}");
            }

            var newIdentity = claimsPrincipal.Identity as ClaimsIdentity;
            newIdentity?.AddClaim(new Claim("Token", jwt));

            _httpContextAccessor.HttpContext.User = claimsPrincipal;
        }

        private static void InitIssuerSigningKey()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();

            string[] ressources = currentAssembly.GetManifestResourceNames();
            string resourceName = GetRessourceName().ToLowerInvariant();

            var certificateResourceName = ressources.Where(x => x.ToLowerInvariant().Contains(resourceName.ToLowerInvariant())).First();

            var resourceStream = currentAssembly.GetManifestResourceStream(certificateResourceName);

            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            using (var ms = new MemoryStream())
            {
                reader.BaseStream.CopyTo(ms);
                _issuerSigningKey = ms.ToArray();
            }
        }

        public static string GetRessourceName()
        {
            switch (_environment)
            {
                case "dev": return "mysmag_portal_dev.cer";

                case "validation": return "mysmag_portal_validation.cer";

                case "preprod": return "mysmag_portal_preprod.cer";

                case "prod": return "mysmag_portal_prod.cer";

                default: return "mysmag_portal_dev.cer";
            }

        }
    }
}