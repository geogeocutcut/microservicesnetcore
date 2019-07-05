using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Core.Common;
using Libragri.AuthenticationDomain.IServices;
using Libragri.AuthenticationDomain.Model;
using Libragri.AuthenticationDomain.Webapi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebapiMongodb.Infrastructure;

namespace Libragri.AuthenticationDomain.Webapi.Controllers
{
    [ApiController]
    public class TokensController : ControllerBase
    {
        
    	private IUserService _userService;
        private IUserRefreshTokenService _refreshTokenService;
        private IOptions<IdentityProviderSettings> _settings;

        public TokensController(IOptions<IdentityProviderSettings> settings,IUserService service,IUserRefreshTokenService refreshTokenService)
    	{
    		_userService = service;
            _settings = settings;
            _refreshTokenService=refreshTokenService;
    	}

        [Route("oauth/token")]
        [HttpPost]
        public async Task<IActionResult> AuthAsync(Parameters parameters)
        {
            if (parameters == null)
            {
                throw new BusinessException("bad request","paramters are missing.");
            }
            if (parameters.client_id == null || parameters.client_id!=_settings.Value.ClientId)
            {
                throw new BusinessException("bad request","bad client id");
            }
            if (parameters.grant_type == "password")
            {
                return Ok(await DoPasswordAsync(parameters));
            }
            else if (parameters.grant_type == "refresh_token")
            {
                return Ok(await DoRefreshTokenAsync(parameters));
            }
            else
            {
                throw new BusinessException("bad request","failed authentication.");
            }
        }

        [Route("oauth/validatetoken")]
        public async Task<IActionResult> ValidateTokenAsync(AuthenticationToken jwt)
        {
            RSACryptoServiceProvider publicAndPrivateKey = RSAKeyProvider.GetPrivateKey();

            var signingKey = new RsaSecurityKey(publicAndPrivateKey);
            
            TokenValidationParameters validationParameters =
            new TokenValidationParameters
            {
                ValidIssuer = _settings.Value.Iss,
                ValidAudience = _settings.Value.Aud,

                IssuerSigningKey = signingKey
            };

            SecurityToken validatedToken;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var user = handler.ValidateToken(jwt.access_token, validationParameters, out validatedToken);
            return Ok(new {message="Token is valid"});
        }

        private async Task<AuthenticationToken> DoPasswordAsync(Parameters parameters)
        {

            var user = await _userService.GetByLoginPassword(parameters.username,parameters.password);                                   


            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");

            var token = new UserRefreshToken
            {
                ClientId = parameters.client_id,
                RefreshToken = refresh_token,
                Id = Guid.NewGuid(),
                User=user
            };

            //store the refresh_token 
            await _refreshTokenService.AddAsync(token);
            return GenerateJwt(parameters.client_id,user, refresh_token, _settings.Value.ExpireMinutes);
            
        }

        //scenario 2 ? get the access_token by refresh_token
        private async Task<AuthenticationToken> DoRefreshTokenAsync(Parameters parameters)
        {
            var token = await _refreshTokenService.GetByRefreshToken(parameters.refresh_token);

            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");

            //expire the old refresh_token and add a new refresh_token
            await _refreshTokenService.DeleteAsync(token.Id);

            await _refreshTokenService.AddAsync(new UserRefreshToken
            {
                ClientId = parameters.client_id,
                RefreshToken = refresh_token,
                Id = Guid.NewGuid(),
                FromDate = DateTime.UtcNow,
                User=token.User
                
            });

            return GenerateJwt(parameters.client_id,token.User, refresh_token, _settings.Value.ExpireMinutes);
        }

        private AuthenticationToken GenerateJwt(string clientId, User user, string refreshToken, int expireMinutes)

        {

            var now = DateTime.UtcNow;



            var claims = new Claim[]

            {

                new Claim(JwtRegisteredClaimNames.Sub, clientId),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),

                new Claim("UserLogin", user.Login),

                new Claim("UserId",user.Id.ToString()),

                new Claim("UserProfiles","["+user.Profiles.Select(p =>p.Name).Aggregate(string.Empty,(p1,p2)=>{
                                                                                                                if(p1==string.Empty) return p2;
                                                                                                                return p1+","+p2;
                                                                                                            })+"]")

            };



            var handler = new JwtSecurityTokenHandler();
            RSACryptoServiceProvider publicAndPrivateKey = RSAKeyProvider.GetPrivateKey();

            var signingKey = new SigningCredentials(new RsaSecurityKey(publicAndPrivateKey), SecurityAlgorithms.RsaSha256);
              
            var jwt = new JwtSecurityToken(claims: claims,  signingCredentials:signingKey);
            
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);



            var response = new AuthenticationToken
            {

                access_token = encodedJwt,

                expires_in = (int)TimeSpan.FromMinutes(expireMinutes).TotalSeconds,

                refresh_token = refreshToken,

            };
            
            return response;
        }
    }
}