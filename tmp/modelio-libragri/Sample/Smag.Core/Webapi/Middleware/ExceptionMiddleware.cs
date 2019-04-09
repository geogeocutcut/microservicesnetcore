using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Smag.Core.Common;
using System;
using System.Diagnostics;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Smag.Core.Webapi.Middleware
{
    /// <summary>
    /// how to use :
    /// In Startup.cs :
    /// public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    /// {
    ///     ...
    ///     app.UseMiddleware<ExceptionHandlerMiddleware>();
    /// }
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(0), ex, ex.Message);

                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = GetHttpStatusCodeForException(ex);
                    await context.Response.WriteAsync(ex.Message);
                }
            }
        }

        private static int GetHttpStatusCodeForException(Exception ex)
        {
            var res = (int)HttpStatusCode.InternalServerError;

            switch (ex)
            {
                case AuthenticationException _:
                    res = (int)HttpStatusCode.Unauthorized;
                    break;

                case NotImplementedException _:
                    res = (int)HttpStatusCode.MethodNotAllowed;
                    break;

                case HttpCallException _:
                    res = (int)HttpStatusCode.BadGateway;
                    break;

                case DalException _:
                    res = (int)HttpStatusCode.BadGateway;
                    break;
            }

            return res;
        }
    }
}