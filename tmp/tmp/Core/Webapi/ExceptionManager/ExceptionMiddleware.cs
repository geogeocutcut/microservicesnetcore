using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Text;
using Core.Common;

namespace Core.Webapi
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                if (ex is ServiceException)
                {
                    //Logger.Error(context.Exception.Message);
                    var resp = JsonConvert.SerializeObject(new { error = "Service Exception : "+((ServiceException)ex).Error, error_description = ex.Message });
                    await context.Response.WriteAsync(resp);
                }
                else if(ex is StoreException)
                {
                    //Logger.Error(context.Exception.Message);
                    var resp = JsonConvert.SerializeObject(new { error = "Store Exception : "+((StoreException)ex).Error, error_description = ex.Message });
                    await context.Response.WriteAsync(resp);
                }
                else
                {
                    var resp = JsonConvert.SerializeObject(new { error = "Unknown Exception ", error_description = ex.Message ,StackOverflowException=ex.StackTrace});
                    await context.Response.WriteAsync(resp);
                }
            }
        }
    }
}

   
