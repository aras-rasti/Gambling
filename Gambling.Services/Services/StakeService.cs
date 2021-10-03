using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gambling.Services.Contracts;
using Gambling.ViewModel.Stake;

namespace Gambling.Services.Services
{
    public class StakeService:IStakeService
    {
        public async Task<StakePlayOutputViewModel> CalculateStakePlayResult(StakePlayInputViewModel input,string playerName)
        {
            Random rnd = new Random();
            var number = rnd.Next(9);
            StakePlayOutputViewModel result = null;

            await Task.Run(() =>
            {
                if (number == input.Number)
                {
                    var point = (9 * input.Point);
                    var account = point + 10000;
                    result = new StakePlayOutputViewModel()
                    {
                        Point = point,
                        Account = account,
                        Status = "Won",
                        PlayerName = playerName
                    };
                }
                else
                {
                    var point = input.Point;
                    var account = 10000 - point;
                    result = new StakePlayOutputViewModel()
                    {
                        Point = point,
                        Account = account,
                        Status = "Lose",
                        PlayerName = playerName
                    };
                }
            });

       

            return result;
        }
    }
}
