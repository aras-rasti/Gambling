using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gambling.Services.Contracts;
using Gambling.ViewModel.User;
using Gambling.WebFramework.Api;

namespace Gambling.Api.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IApplicationUserManager _userManager;

        public UserController(IApplicationUserManager userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<ApiResult<string>> SignIn(SignInInputViewModel input)
        {
            var user =await _userManager.FindByNameAsync(input.UserName);
            return user.FirstName;
        }
    }
}
