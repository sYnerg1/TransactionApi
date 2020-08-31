using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LegioTest.Api.Models.Account;
using LegioTest.Domain.ModelsDTO;
using LegioTest.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LegioTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _users;

        public AccountController(IUserService users)
        {
            _users = users;
        }

        /// <summary>
        /// Register new User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     api\Registration
        ///       {
        ///        "username" : "testlogin",
        ///        "password": "Testpass"
        ///        }
        /// </remarks>
        /// <response code="201">User registered</response> 
        /// <response code="400">If user instance is null</response> 
        /// <response code="500">Server error</response> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("Registration")]
        public async Task<IActionResult> Post(RegisterUserModel model)
        {
            if(model==null)
            {
                return BadRequest("null argument");
            }

            UserDTO user = new UserDTO()
            {
                UserName = model.UserName,
                Password = model.Password
            };

            try
            {
                var result = await _users.CreateUser(user);
                return StatusCode(201, result.ToString());
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        /// <summary>
        /// Login and get JWT.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     api\Login
        ///       {
        ///        "username" : "testlogin",
        ///        "password": "Testpass"
        ///        }
        /// </remarks>
        /// <response code="200">New istance of JWT</response> 
        /// <response code="400">If user instance is null</response> 
        /// <response code="500">Server error</response> 
        [HttpPost("Login")]
        public async Task<object> Post(LoginUserModel model)
        {
            if (model == null)
            {
                return BadRequest("null argument");
            }

            UserDTO dto = new UserDTO()
            {
                UserName = model.UserName,
                Password = model.Password
            };

            try
            {
                var result = await _users.SignIn(dto);

                if (result.Succeeded)
                {
                    var user = await _users.FindUserByName(dto.UserName);
                    return await _users.GetJWT(user);
                }

                return BadRequest(result.ToString());
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }    
        }
    }
}
