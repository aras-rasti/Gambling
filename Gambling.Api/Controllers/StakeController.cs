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
    /// <summary>
    /// This controller handel the game
    /// </summary>
    public class StakeController : BaseApiController
    {
        private readonly IStakeService _stakeService;

        public StakeController(IStakeService stakeService)
        {
            _stakeService = stakeService;
        }

        /// <summary>
        /// Game Instructions :
        /// 1- Game of chance in which a random number between 0 - 9 is to be generated.
        ///2- The player has a starting account of 10,000 points and can use any
        /// Bet the partial amount on the randomly generated number.
        ///3- If he is correct, he gets 9 times his stake as a profit.
        /// </summary>
        /// <param name="input">Point should be between 1 and 10000 and
        ///Number should be between 0 and 9
        /// </param>
        /// <returns></returns>
        [HttpPost("StakePlay")]
        [Authorize]
        public async Task<ApiResult<StakePlayOutputViewModel>> StakePlay(StakePlayInputViewModel input)
        {
            var userName = HttpContext.User.Identity.Name;
           return await _stakeService.CalculateStakePlayResult(input, userName);
        }
    }
}
