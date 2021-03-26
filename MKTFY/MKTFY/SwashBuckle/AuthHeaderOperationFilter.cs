using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.SwashBuckle
{
    /// <summary>
    /// This class is to add the SwashBuckle capability to add authorization to the endpoints so endpoints can be tested
    /// </summary>
    public class AuthHeaderOperationFilter : IOperationFilter
    {
        /// <summary>
        /// AuthHeaderOperationFilter Constructor
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Only authorize the endpoint if it has an Authorize attribute
            var isAuthorized = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() ||
                            context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();
            if (!isAuthorized) return;

            // Add the Access Token parameter to the endpoint documentation
            if (operation.Security == null)
                operation.Security = new List<OpenApiSecurityRequirement>();

            var scheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
            };

            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [scheme] = new List<string>()
            });
        }
    }
}
