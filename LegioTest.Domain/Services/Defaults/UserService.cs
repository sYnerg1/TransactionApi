using LegioTest.Data.Repositories;
using LegioTest.Domain.ModelsDTO;
using LegioTest.Domain.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LegioTest.Domain.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _users;
        private readonly IOptions<JwtOption> _jwtOptions;

        public UserService(IUserRepository users, IOptions<JwtOption> jwtOptions)
        {
            _users = users;
            _jwtOptions = jwtOptions;
        }

        public async Task<IdentityResult> CreateUser(UserDTO user)
        {

            IdentityUser newUser = new IdentityUser()
            {
                UserName = user.UserName,
            };

            if (await _users.GetUserByUserName(user.UserName) == null)
            {
                return await _users.AddUserAsync(newUser, user.Password);
            }
            else
            {
                throw new Exception("User already exist");
            }

        }

        public async Task<SignInResult> SignIn(UserDTO user)
        {

            return await _users.SignInAsync(user.UserName, user.Password);
        }

        public async Task<UserDTO> FindUserByName(string userName)
        {
            var existUser = await _users.GetUserByUserName(userName);

            return new UserDTO() { UserName = existUser.UserName };
        }

        public async Task<object> GetJWT(UserDTO user)
        {
            var tokenTimeCreated = DateTime.UtcNow;

            var claims = new List<Claim>
                {
                    new Claim("UserName", user.UserName)
                };

            string secretKey = _jwtOptions.Value.Key;
            int expires = _jwtOptions.Value.Expires;

            var jwt = new JwtSecurityToken(
                    issuer: "Me",
                    audience: "My Api",
                    notBefore: tokenTimeCreated,
                    claims: claims,
                    expires: tokenTimeCreated.AddMinutes(expires),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(secretKey)),
                        SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
   
}
}
