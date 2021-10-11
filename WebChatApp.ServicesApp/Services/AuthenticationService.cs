using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebChatApp.Core;
using WebChatApp.Core.Enum;
using WebChatApp.Core.Session;
using WebChatApp.Models.Entities;
using WebChatApp.Models.Models;
using WebChatApp.Models.RelationShip;

namespace WebChatApp.ServicesApp
{
    public class AuthenticationService : IAuthenticationService
    {
        private ISession _session;
        private IUserService _userService;
        public AuthenticationService(ISession session, IUserService userService)
        {
            _session = session;
            _userService = userService;
        }
        public async Task<UserEntity> GetAuthentificatedUser(string login)
        { 
            return await _session.Query<UserEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Login == login );

        }
        public AuthResponse GenerateToken(UserEntity user)
        {
            var identity = GetIdentity(user);
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            
            return new AuthResponse
            {
                Token = encodedJwt,
                UserName = identity.Name,
                Roles = user.Roles
            };
        }
        private ClaimsIdentity GetIdentity(UserEntity user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim("id", user.Id.ToString())
                };
            foreach (UserRole userRole in user.Roles)         
            {
                var roleName = userRole.Role.Name;
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName));
            }
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

    }
}
