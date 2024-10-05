using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inf_api.Middlewares;

public class GlobalExceptionHandlingMiddleware:IMiddleware
{
    private  ILogger<GlobalExceptionHandlingMiddleware> _logger ;
    
    
    public  GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        this._logger=logger;
    }

    public async Task  InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try{
        await next(context);
        }catch(Exception e){
            _logger.LogError(e,e.Message);
            ProblemDetails problem = new   ProblemDetails(){
                Status=(int)HttpStatusCode.InternalServerError,
                Type="Server Error",
                Title="Server Error",
                Detail="An internal server has occurred"
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType="application/problem+json";
            await context.Response.WriteAsync(json);
        }
    }
} 