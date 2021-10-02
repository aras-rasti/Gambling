using System.Collections.Generic;
using System.Linq;
using Gambling.Common;
using Gambling.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gambling.WebFramework.Filters
{
    
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult okObjectResult)
            {
                var apiResult = new ApiResult<object>(true, ApiResultStatusCode.Success, okObjectResult.Value);
                context.Result = new JsonResult(apiResult) { StatusCode = okObjectResult.StatusCode };
            }
            else if (context.Result is OkResult okResult)
            {
                var apiResult = new ApiResult(true, ApiResultStatusCode.Success);
                context.Result = new JsonResult(apiResult) { StatusCode = okResult.StatusCode };
            }
            //return BadRequest() method create an ObjectResult with StatusCode 400 in recent versions, So the following code has changed a bit.
            else if (context.Result is ObjectResult badRequestObjectResult && badRequestObjectResult.StatusCode == 400)
            {
                List<string> message = new List<string>();
                switch (badRequestObjectResult.Value)
                {
                    case ValidationProblemDetails validationProblemDetails:
                        var errorMessages = validationProblemDetails.Errors.SelectMany(p => p.Value).Distinct();
                        message.Add( string.Join(" | ", errorMessages));
                        break;
                    case SerializableError errors:
                        var errorMessages2 = errors.SelectMany(p => (string[])p.Value).Distinct();
                        message.Add(string.Join(" | ", errorMessages2));
                        break;
                    case var value when value != null && !(value is ProblemDetails):
                        message.Add(badRequestObjectResult.Value.ToString());
                        break;
                }

                var apiResult = new ApiResult(false, ApiResultStatusCode.BadRequest, message);
                context.Result = new JsonResult(apiResult) { StatusCode = badRequestObjectResult.StatusCode };
            }
            else if (context.Result is ObjectResult notFoundObjectResult && notFoundObjectResult.StatusCode == 404)
            {
                List<string> message = new List<string>();
                if (notFoundObjectResult.Value != null && !(notFoundObjectResult.Value is ProblemDetails))
                    message.Add(notFoundObjectResult.Value.ToString());

                //var apiResult = new ApiResult<object>(false, ApiResultStatusCode.NotFound, notFoundObjectResult.Value);
                var apiResult = new ApiResult(false, ApiResultStatusCode.NotFound, message);
                context.Result = new JsonResult(apiResult) { StatusCode = notFoundObjectResult.StatusCode };
            }
            else if (context.Result is ContentResult contentResult)
            {
                List<string> message = new List<string>() { contentResult.Content };
                var apiResult = new ApiResult(true, ApiResultStatusCode.Success, message);
                context.Result = new JsonResult(apiResult) { StatusCode = contentResult.StatusCode };
            }
            else if (context.Result is ObjectResult objectResult && objectResult.StatusCode == null
                && !(objectResult.Value is ApiResult))
            {
                var apiResult = new ApiResult<object>(true, ApiResultStatusCode.Success, objectResult.Value);
                context.Result = new JsonResult(apiResult) { StatusCode = objectResult.StatusCode };
            }

            base.OnResultExecuting(context);
        }
    }
}
