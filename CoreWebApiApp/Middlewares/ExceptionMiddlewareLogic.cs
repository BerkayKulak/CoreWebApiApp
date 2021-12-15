using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CoreWebApiApp.Middlewares
{
    public class ExceptionInfo
    {
        public int ExceptionId { get; set; }
        public string ExceptionMessage { get; set; }
    }
    public class ExceptionMiddlewareLogic
    {
        private readonly RequestDelegate _request;
        public ExceptionMiddlewareLogic(RequestDelegate request)
        {
            _request = request;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception e)
            {
                await ExceptionLogic(context, e);
            }
        }

        private async Task ExceptionLogic(HttpContext ctx , Exception ex)
        {
            ctx.Response.StatusCode = 500;

            string message = ex.Message;

            var exceptionInfo = new ExceptionInfo()
            {
                ExceptionId = ctx.Response.StatusCode,
                ExceptionMessage = message
            };

            string responseMessage = JsonConvert.SerializeObject(exceptionInfo);

            await ctx.Response.WriteAsync(responseMessage);
        }
    }

    public static class ApplyMiddleware
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddlewareLogic>();

        }
    }
}
