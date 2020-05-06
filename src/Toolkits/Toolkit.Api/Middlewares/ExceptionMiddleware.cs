using log4net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Toolkit.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILog _logger = LogManager.GetLogger(typeof(ExceptionMiddleware));

        public ExceptionMiddleware(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, int errorCode = (int)HttpStatusCode.InternalServerError)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorCode;
            _logger.Error("Error", exception);
            await context.Response.WriteAsync(
                JsonConvert.SerializeObject(new
                {
                    Message = new List<string> { "Error in application. Please contact administrator." },
                    Succeeded = false
                })
            );
        }
    }
}
