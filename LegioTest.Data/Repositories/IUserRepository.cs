using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LegioTest.Data.Repositories
{
    public interface IUserRepository
    {
        Task<IdentityResult> AddUserAsync(IdentityUser value, string password);
        Task<IdentityUser> GetUserByUserName(string userName);
        Task<SignInResult> SignInAsync(string userName, string password);
    }
}
