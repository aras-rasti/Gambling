using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gambling.ViewModel.Stake;

namespace Gambling.Services.Contracts
{
    public interface IStakeService
    {
         Task<StakePlayOutputViewModel> CalculateStakePlayResult(StakePlayInputViewModel input, string playerName);
    }
}
