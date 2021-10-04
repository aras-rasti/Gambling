﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Gambling.Common;
using Gambling.Common.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Gambling.WebFramework.Configuration.DI
{
    public static class AddCustomAuthenticationExtentions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services,
            SiteSettings siteSettings)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var secretkey = Encoding.UTF8.GetBytes(siteSettings.JwtSettings.SecretKey);

                    var validationParameters = new TokenValidationParameters
                    {
                        RequireSignedTokens = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretkey),

                        RequireExpirationTime = true,
                        ValidateLifetime = true,

                        ValidateAudience = true, 
                        ValidAudience = siteSettings.JwtSettings.Audience,

                        ValidateIssuer = true, 
                        ValidIssuer = siteSettings.JwtSettings.Issuer
                    };

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = validationParameters;
                    //options.Events = new JwtBearerEvents
                    //{
                    //    OnAuthenticationFailed = context =>
                    //    {
                    //        if (context.Exception != null)
                    //            throw new AppException(ApiResultStatusCode.UnAuthorized, "Authentication failed.", HttpStatusCode.Unauthorized, context.Exception, null);
                    //        return Task.CompletedTask;
                    //    },

                    //    //OnTokenValidated = async context =>
                    //    //{
                    //    //    var userRepository = context.HttpContext.RequestServices.GetRequiredService<IApplicationUserManager>();

                    //    //    var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                    //    //    if (claimsIdentity.Claims?.Any() != true)
                    //    //        context.Fail("This token has no claims.");

                    //    //    var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
                    //    //    if (!securityStamp.HasValue())
                    //    //        context.Fail("This token has no secuirty stamp");

                    //    //    var userId = claimsIdentity.GetUserId<string>();
                    //    //    var user = await userRepository.GetUserAsync(context.Principal);

                    //    //    if (user.SecurityStamp != securityStamp)
                    //    //        context.Fail("Token secuirty stamp is not valid.");

                    //    //    if (!user.IsActive)
                    //    //        context.Fail("User is not active.");
                    //    //},

                    //    OnChallenge = context =>
                    //    {
                    //        if (context.AuthenticateFailure != null)
                    //            throw new AppException(ApiResultStatusCode.UnAuthorized, "Authenticate failure.", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
                    //        throw new AppException(ApiResultStatusCode.UnAuthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);
                    //    }

                    //};
                });
            return services;
        }
    }
}