using BuisnessEntities;
using BuisnessObject.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CustomPipeline.Authentication
{
    public class GepAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IToken _token;
        // private readonly IServiceError _serviceError;
        //private readonly IJWTToken _jwtToken;
        private List<string> anonymousController = new List<string>() { "healthcheck", "common","weatherforecast" };

        public GepAuthorizationMiddleware(RequestDelegate next, IToken token)
        {
            _next = next;
            _token = token;
            //_serviceError = serviceError;
        }

        //public async Task Invoke(HttpContext httpContext)
        //{
        //    await _next(httpContext);
        //}

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                var route = httpContext.GetRouteData();
                bool isAuthorized = true;

                if (route != null)
                {
                    isAuthorized = false;
                    string authHeader = httpContext.Request.Headers["Authorization"];
                    string controllerName = route.Values["Controller"] != null ? route.Values["Controller"].ToString() : string.Empty;

                    if (anonymousController.Contains(controllerName.ToLower()))
                    {
                        isAuthorized = true;
                    }
                    else if (authHeader != null)
                    {

                        var userContext = _token.GetUsercontext("Valid", authHeader);
                        isAuthorized = true;
                    }


                    else
                    {
                        GenerateResponse(httpContext, StatusCodes.Status401Unauthorized, "Un-Authorization", "Un-Authorized Access").ConfigureAwait(false).GetAwaiter().GetResult();
                    }
                }

                if (isAuthorized)
                {
                    await _next(httpContext);
                }

            }
            catch (Exception exception)
            {
                //_serviceError.IsSystemErrorOccured = true;
                //_serviceError.SmartLogError(exception, GetType().Namespace + " - Error Occured in " + MethodBase.GetCurrentMethod());

                if (exception is SecurityTokenException || exception is CryptographicException || exception is FormatException)
                {
                    GenerateResponse(httpContext, StatusCodes.Status401Unauthorized, "Un-Authorization", "Un-Authorized Access").ConfigureAwait(false).GetAwaiter().GetResult();
                }
                else
                {
                    GenerateResponse(httpContext, StatusCodes.Status500InternalServerError, "System-Error", "System Error Occured").ConfigureAwait(false).GetAwaiter().GetResult();
                }
            }
        }

        public async Task GenerateResponse(HttpContext httpContext, int statusCode, string errorCode, string errorDescription)
        {
            var result = new UnauthorizedObjectResult(new ResponseModel()
            {
                Data = null,
                StatusCode = statusCode.ToString(),
                ErrorDetails = new List<ErrorModel>()
                {
                    new ErrorModel()
                    {
                        Key = "",
                        Errors = new List<Error>()
                        {
                            new Error()
                            {
                                ErrorCode = errorCode,
                                ErrorDescription = errorDescription
                            }
                        }
                    }
                }
            });

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(result.Value));
        }
    }
    public static class CalculatorAuthentication
    {
        public static IApplicationBuilder CalculatorAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GepAuthorizationMiddleware>(); 
        }
    }
    public static class GepAuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseGepAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GepAuthorizationMiddleware>();
        }
    }
}

