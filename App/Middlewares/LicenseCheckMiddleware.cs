using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Project.App.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Project.App.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LicenseCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration Configuration;

        public LicenseCheckMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            Configuration = configuration;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (Configuration["LicenseSettings:SecretKey"] is null || !Configuration["LicenseSettings:SecretKey"].IsBase64())
            {
                await HandleResponse(httpContext, ResponseCode.LicenseInValid);
                return;
            }
            string[] data = Encoding.UTF8.GetString(Convert.FromBase64String(Configuration["LicenseSettings:SecretKey"])).Split("|");
            if(data.Length != 2 || !DateTime.TryParseExact(data[0], "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime expireDate))
            {
                await HandleResponse(httpContext, ResponseCode.LicenseInValid);
                return;
            }

            RSACryptoServiceProvider csp = new(2048);
            csp.ImportFromPem(Configurations.RSAPublicKey.ToCharArray());

            byte[] bytesData = Encoding.UTF8.GetBytes(data[0]!);
            byte[] signData = Convert.FromBase64String(data[1]);
            if(!csp.VerifyData(bytesData, CryptoConfig.MapNameToOID("SHA512")!, signData))
            {
                await HandleResponse(httpContext, ResponseCode.LicenseInValid);
                return;
            }
            if(expireDate < DateTime.Now)
            {
                await HandleResponse(httpContext, ResponseCode.LicenseDataExpire);
                return;
            }
            await _next(httpContext);
        }

        private async Task HandleResponse(HttpContext httpContext, string message)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
            JObject responseData = new()
            {
                ["statusCode"] = httpContext.Response.StatusCode,
                ["message"] = message
            };
            await httpContext.Response.WriteAsync(responseData.ToString());
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LicenseCheckMiddlewareExtensions
    {
        public static IApplicationBuilder UseLicenseCheckMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LicenseCheckMiddleware>();
        }
    }
}
