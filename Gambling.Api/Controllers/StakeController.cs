using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gambling.Services.Contracts;
using Gambling.ViewModel.Stake;
using Gambling.WebFramework.Api;
using Microsoft.AspNetCore.Authorization;

namespace Gambling.Api.Controllers
{
    public class StakeController : BaseApiController
    {
        private readonly IStakeService _stakeService;

        public StakeController(IStakeService stakeService)
        {
            _stakeService = stakeService;
        }
        [HttpPost("StakePlay")]
        [Authorize]
        public async Task<ApiResult<StakePlayOutputViewModel>> StakePlay(StakePlayInputViewModel input)
        {
            var userName = HttpContext.User.Identity.Name;
           return await _stakeService.CalculateStakePlayResult(input, userName);
        }
    }
}
