using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambling.Common
{
    public enum ApiResultStatusCode
    {
        [Display(Name = "Operation done successfully !")]
        Success = 0,

        [Display(Name = "An error occurred on the server !")]
        ServerError = 1,

        [Display(Name = "Parameters are not valid")]
        BadRequest = 2,

        [Display(Name = "NotFound !")]
        NotFound = 3,

        [Display(Name = "The list is empty !")]
        ListEmpty = 4,

        [Display(Name = "Logic Error occurred !")]
        LogicError = 5,

        [Display(Name = "Authorization  Error occurred !")]
        UnAuthorized = 6
    }
}
