using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Gambling.Common;
using Gambling.WebFramework.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Gambling.WebFramework.Middlewares
{
    public static class CustomExceptionHandlerMiddlewareExtentions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }

    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _evn;
        public CustomExceptionHandlerMiddleware(RequestDelegate next, IHostingEnvironment evn)
        {
            _next = next;
            _evn = evn;
        }

        public async Task Invoke(HttpContext context)
        {
            List<string> Message = new List<string>();
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            ApiResultStatusCode apiResultStatus = ApiResultStatusCode.ServerError;

            try
            {
                await _next(context);
            }

            catch (Exception exception)
            {
                if (_evn.IsDevelopment())
                {
                    var error = new Dictionary<string, string>
                    {
                        ["Exception"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace,
                    };
                    Message.Add(JsonConvert.SerializeObject(error));
                }

                else
                {
                    Message.Add("An error has occurred.");
                }

                await WriteToResponseAsync();
            }

            async Task WriteToResponseAsync()
            {
                var result = new ApiResult(false, apiResultStatus, Message);
                var jsonResult = JsonConvert.SerializeObject(result);

                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonResult);
            }
        }
    }
}
