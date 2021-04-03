using Microsoft.AspNetCore.Http;
using MKTFY.App.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace MKTFY.Middleware
{
    /// <summary>
    /// Global Exception Handler
    /// </summary>
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// GlobalExceptionHandler Constructor
        /// </summary>
        /// <param name="next">Takes in a RequestDelegate parameter</param>
        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// Invoke for GlobalExeptionHandler
        /// </summary>
        /// <param name="context">Takes in HttpContext</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                string errorMessage = null;

                switch (ex)
                {
                    case NotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        errorMessage = e.Message;
                        break;
                    case PasswordValidationException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorMessage = e.Message;
                        break;
                    case EmailVerificationException e:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorMessage = e.Message;
                        break;
                    default: // Some unknown error. We want to prevent generic 500 errors from being returned.
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorMessage = "We are sorry, your request could not be completed";
                        break;
                }

                // Return the response
                var result = JsonSerializer.Serialize(new { message = errorMessage });
                await response.WriteAsync(result);
            }
        }
    }
}