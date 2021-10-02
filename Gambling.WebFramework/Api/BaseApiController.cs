using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Gambling.WebFramework.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Gambling.WebFramework.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiResultFilter]
    public class BaseApiController: ControllerBase
    {
        public BaseApiController() : base()
        {
        }
    }
}
