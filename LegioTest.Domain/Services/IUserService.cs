using LegioTest.Domain.ModelsDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LegioTest.Domain.Services
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUser(UserDTO user);
        Task<SignInResult> SignIn(UserDTO user);
        Task<UserDTO> FindUserByName(string userName);
        Task<object> GetJWT(UserDTO user);
    }
}
