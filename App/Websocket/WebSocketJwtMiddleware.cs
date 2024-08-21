//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using Newtonsoft.Json.Linq;
//using Project.App.DesignPatterns.Reponsitories;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Net;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace Project.App.Websocket
//{
//    public class WebSocketJwtMiddleware
//    {
//        //private readonly RequestDelegate _next;
//        //public IConfiguration Configuration { get; }
//        //private readonly IRepositoryWrapperMariaDB RepositoryWrapperMariaDB;
//        //public WebSocketJwtMiddleware(RequestDelegate next, IConfiguration configuration, IRepositoryWrapperMariaDB repositoryWrapperMariaDB)
//        //{
//        //    _next = next;
//        //    Configuration = configuration;
//        //    RepositoryWrapperMariaDB = repositoryWrapperMariaDB;
//        //}
//        //public async Task Invoke(HttpContext context)
//        //{
//        //    if (context.WebSockets.IsWebSocketRequest)
//        //    {
//        //        if (TryGetJwtFromHeaders(context, out string jwtToken))
//        //        {
//        //            if (ValidateJwt(jwtToken, out ClaimsPrincipal principal))
//        //            {
//        //                // Attach the authenticated user principal to the HttpContext
//        //                context.User = principal;
//        //                Device device = RepositoryWrapperMariaDB.Devices.FindByCondition(x => x.DeviceId.Equals(context.User.Claims.FirstOrDefault().Value)).Include(x => x.Area).FirstOrDefault();
//        //                if (device is null)
//        //                {
//        //                    await HandleResponse(context, "Device not found");
//        //                    return;
//        //                }
//        //                if (device.DeviceStatus == DeviceStatus.DeActive)
//        //                {
//        //                    await HandleResponse(context, "Device is deactive");
//        //                    return;
//        //                }
//        //                if(device.Area.AreaStatus == Modules.Areas.Entities.AreaStatus.Deactive)
//        //                {
//        //                    await HandleResponse(context, "Device Area is deactive");
//        //                }
//        //            }
//        //            else
//        //            {
//        //                await HandleResponse(context,"Invalid JWT");
//        //                return;
//        //            }
//        //        }
//        //        else
//        //        {
//        //            await HandleResponse(context, "Missing JWT");
//        //            return;
//        //        }
//        //    }

//        //    await _next(context);
//        //}

//        //private async Task HandleResponse(HttpContext httpContext, string message)
//        //{
//        //    httpContext.Response.ContentType = "application/json";
//        //    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
//        //    JObject responseData = new()
//        //    {
//        //        ["statusCode"] = httpContext.Response.StatusCode,
//        //        ["message"] = message
//        //    };
//        //    await httpContext.Response.WriteAsync(responseData.ToString());
//        //}
//        //private bool TryGetJwtFromHeaders(HttpContext context, out string jwtToken)
//        //{
//        //    jwtToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//        //    return !string.IsNullOrEmpty(jwtToken);
//        //}

//        //private bool ValidateJwt(string jwtToken, out ClaimsPrincipal principal)
//        //{
//        //    var tokenHandler = new JwtSecurityTokenHandler();
//        //    var validationParameters = new TokenValidationParameters
//        //    {
//        //        ValidateIssuerSigningKey = true,
//        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:SecretKey"])), // Replace with your actual signing key
//        //        ValidateIssuer = false, // Optionally validate the issuer
//        //        ValidateAudience = false // Optionally validate the audience
//        //    };

//        //    try
//        //    {
//        //        principal = tokenHandler.ValidateToken(jwtToken, validationParameters, out _);
//        //        return true;
//        //    }
//        //    catch (Exception)
//        //    {
//        //        principal = null;
//        //        return false;
//        //    }
//        //}
//    }
//}
