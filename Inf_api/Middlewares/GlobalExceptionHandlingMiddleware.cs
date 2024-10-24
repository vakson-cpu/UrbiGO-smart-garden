using System.Net;
using System.Text.Json;
using Inf_Transfer.utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inf_api.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                var response = context.Response;
                response.ContentType = "application/json";

                // Use switch expression for handling exception types and setting status code
                var (customResponse, statusCode) = HandleException(e);

                response.StatusCode = statusCode;
                string json = JsonSerializer.Serialize(customResponse);
                await response.WriteAsync(json);
            }
        }

        private (CustomResponse<string> response, int statusCode) HandleException(Exception e)
        {
            return e switch
            {
                ArgumentNullException _ => (
                    new CustomResponse<string>(
                        message: "Required argument is missing.",
                        succeeded: false,
                        data: null
                    ),
                    (int)HttpStatusCode.BadRequest
                ),

                UnauthorizedAccessException _ => (
                    new CustomResponse<string>(
                        message: "Unauthorized access.",
                        succeeded: false,
                        data: null
                    ),
                    (int)HttpStatusCode.Unauthorized
                ),

                Exception customEx => (
                    new CustomResponse<string>(
                        message: customEx.Message,
                        succeeded: false,
                        data: null
                    ),
                    (int)HttpStatusCode.BadRequest
                ),

                _ => (
                    new CustomResponse<string>(
                        message: "An internal server error has occurred.",
                        succeeded: false,
                        data: null
                    ),
                    (int)HttpStatusCode.InternalServerError
                )
            };
        }
    }
}

