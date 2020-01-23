﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace CompanyAPI.Configuration
{
    public static class InvalidRequestStateConfiguration
    {
        public static IServiceCollection RegisterRequestState(this IServiceCollection services)
        {
            // override Model State
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState
                                        .Values
                                        .SelectMany(x => x.Errors.Select(e => e.ErrorMessage))
                                        .ToList();

                    var result = new
                    {
                        Message = "Validation Errors",
                        Errors = errors
                    };

                    return new BadRequestObjectResult(result);
                };
            });
            return services;
        }
    }
}
