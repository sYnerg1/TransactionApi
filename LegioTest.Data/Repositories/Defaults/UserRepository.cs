using LegioTest.Data.EntityFramerwork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LegioTest.Data.Repositories.Defaults
{
   public  class UserRepository :IUserRepository
    {
        private readonly LegioContext _db;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(LegioContext db,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;

        }

        public async Task<IdentityResult> AddUserAsync(IdentityUser value, string password)
        {
            return await _userManager.CreateAsync(value, password);
        }

        public async Task<SignInResult> SignInAsync(string userName, string password)
        {
            return await _signInManager.PasswordSignInAsync(userName, password, false, false);
        }

        public async Task<IdentityUser> GetUserByUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
    }
}
