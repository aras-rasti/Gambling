using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambling.ViewModel.Stake
{
    public class StakePlayOutputViewModel
    {
        public int Account { get; set; }
        public int Point { get; set; }
        public string Status { get; set; }
        public string PlayerName { get; set; }
    }
}
