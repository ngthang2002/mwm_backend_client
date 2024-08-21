using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Project.App.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate RequestDelegate;

        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate)
        {
            RequestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine(DateTime.Now.ToString() + " " + httpContext.Request.Path.ToString());
            using MemoryStream memoryStream = new();
            Stream originalResponseBody = httpContext.Response.Body;
            httpContext.Response.Body = memoryStream;

            try
            {
                await RequestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                JObject responseData = new()
                {
                    ["statusCode"] = httpContext.Response.StatusCode,
                    ["message"] = ex.GetBaseException().ToString()
                };
                await httpContext.Response.WriteAsync(responseData.ToString());
            }
            finally
            {
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.BadRequest)
                {
                    memoryStream.Position = 0;
                    string responseOriginData = await new StreamReader(memoryStream).ReadToEndAsync();
                    (bool resultParse, JToken dataResponse) = IsValidJson(responseOriginData);
                    if (resultParse && dataResponse["errors"] != null)
                    {
                        httpContext.Response.Body = originalResponseBody;
                        httpContext.Response.ContentLength = null;
                        Dictionary<string, List<string>> errors = dataResponse["errors"].ToObject<Dictionary<string, List<string>>>();
                        JObject responseData = new()
                        {
                            ["statusCode"] = httpContext.Response.StatusCode,
                            ["message"] = JToken.FromObject(errors.FirstOrDefault().Value[0])
                        };
                        await httpContext.Response.WriteAsync(responseData.ToString());
                    }
                    else
                    {
                        memoryStream.Position = 0;
                        await memoryStream.CopyToAsync(originalResponseBody);
                        httpContext.Response.Body = originalResponseBody;
                    }
                }
                else
                {
                    memoryStream.Position = 0;
                    await memoryStream.CopyToAsync(originalResponseBody);
                    httpContext.Response.Body = originalResponseBody;
                }
            }
        }

        private static (bool resultParse, JToken dataParse) IsValidJson(string strInput)
        {
            try
            {
                JToken token = JContainer.Parse(strInput);
                return (true, token);
            }
            catch (Exception)
            {
                return (false, null);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
