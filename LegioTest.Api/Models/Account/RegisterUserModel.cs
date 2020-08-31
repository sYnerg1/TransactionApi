using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegioTest.Api.Models.Account
{
    public class RegisterUserModel
    {
        /// <summary>
        /// Username of new user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// New user password
        /// </summary>
        public string Password { get; set; }
    }
}
