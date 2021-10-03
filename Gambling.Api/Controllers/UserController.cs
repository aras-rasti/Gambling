using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gambling.Services.Contracts;
using Gambling.ViewModel.User;
using Gambling.WebFramework.Api;
using Microsoft.AspNetCore.Authorization;

namespace Gambling.Api.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IJwtService _jwtService;

        public UserController(IApplicationUserManager userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }
        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<ApiResult<string>> SignIn(SignInInputViewModel input)
        {
            var user =await _userManager.FindByUserNameAsync(input.UserName);

            if (user == null)
                return BadRequest("UserName or Password is wrong !");
            else
            {
                var result = await _userManager.CheckPasswordAsync(user, input.Password);
                if (result)
                    return Ok(await _jwtService.GenerateTokenAsync(user));
                else
                    return BadRequest("UserName or Password is wrong !");
            }
        }
    }
}
