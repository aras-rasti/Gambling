using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambling.ViewModel.Stake
{
    public class StakePlayInputViewModel
    {
        [Required(ErrorMessage = "{0} is required!")]
        [Range(1, 10000, ErrorMessage = "Please enter {0} Between 1 and 10000 !")]
        public int Point { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [Range(0, 9, ErrorMessage = "Please enter {0} Between 0 and 9 !")]
        public short Number { get; set; }
    }
}
