//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc.Authorization;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Newtonsoft.Json.Linq;
//using Project.App.DesignPatterns.Reponsitories;
//using Project.App.Helpers;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Security.Claims;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace Project.App.Middlewares
//{
//    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
//    public class UserHandlerMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly IServiceScopeFactory ServiceScopeFactory;
//        private readonly List<string> ExceptPath = new List<string>()
//        {
//            "/api/Schedule/device/schedule",
//            "/api/mqtt/dangkythietbi",
//            "/api/Devices/info",
//            "/api/area",
//            "/api/Schedule/httt",
//            "/api/Devices/report",
//            "/api/Area/*/UpdateLicense"
//        };
//        //private readonly IAreaService AreaService;

//        //public UserHandlerMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory, IAreaService areaService)
//        //{
//        //    _next = next;
//        //    ServiceScopeFactory = serviceScopeFactory;
//        //    AreaService = areaService;
//        //}

//        private bool hasAllowAnonymous(HttpContext httpContext)
//        {
//            var endpoint = httpContext.GetEndpoint();
//            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
//            {
//                return true;
//            }

//            return false;
//        }

//        public async Task Invoke(HttpContext httpContext)
//        {
//            if (ExceptPath.Any(x => {
//                return Regex.IsMatch(httpContext.Request.Path.ToString(), "^" + x + "$");
//            }))
//            {
//                await _next(httpContext);
//            }
//            else
//            {
//                if (!this.hasAllowAnonymous(httpContext) && httpContext.User.Identity.IsAuthenticated)
//                {
//                    string userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
//                    List<string> currentRole = httpContext.User.FindAll(ClaimTypes.Role).Select(s => s.Value).ToList();
//                    using IServiceScope serviceScope = ServiceScopeFactory.CreateScope();
//                    IRepositoryWrapperMariaDB MariaDBContext = serviceScope.ServiceProvider.GetRequiredService<IRepositoryWrapperMariaDB>();
//                    Account account = await MariaDBContext.Accounts
//                        .FindByCondition(x => x.AccountId.Equals(userId))
//                        .Include(x => x.Area)
//                        .ThenInclude(x => x.ParentArea)
//                        .Include(x => x.Role)
//                        .ThenInclude(x => x.RolePermissions)
//                        .FirstOrDefaultAsync();
//                    AccountTw accountTw = await MariaDBContext.AccountTws.FindByCondition(x => x.AccountTwId.Equals(userId)).FirstOrDefaultAsync();
//                    VendorInfo vendorInfo = await MariaDBContext.VendorInfos.FindByCondition(x => x.VendorInfoId.Equals(userId)).FirstOrDefaultAsync();
//                    Area areaRoot = null;
//                    if (account is not null)
//                    {
//                        areaRoot = await AreaService.GetRoot(account.AreaId);
//                    }
//                    if (accountTw is not null || vendorInfo is not null)
//                    {
//                        await _next(httpContext);
//                    }
//                    else if (account is null)
//                    {
//                        await HandleResponse(httpContext, "UserDoesNotExists");
//                    }
//                    else if (account.Area is not null && account.Area.AreaStatus == AreaStatus.Deactive)
//                    {
//                         await HandleResponse(httpContext, "AreaDeactive");
//                    }
//                    else if(areaRoot is not null && areaRoot.LicenseExpirationAt.HasValue && areaRoot.LicenseExpirationAt < DateTime.Now)
//                    {
//                        await HandleResponse(httpContext, ResponseCode.LicenseExpirationAtHasExpired);
//                    }    
//                    else if (MariaDBContext.AreaRelations.Any(x => x.ChildId.Equals(account.AreaId) && x.Parent.AreaStatus == AreaStatus.Deactive))
//                    {
//                        await HandleResponse(httpContext, "ParentAreaDeactive");
//                    }
//                    else if (account.Role is not null && account.Role.RolePermissions.Count != currentRole.Count)
//                    {
//                        await HandleResponse(httpContext, "RoleIsChange");
//                    }
//                    else
//                    {
//                        await _next(httpContext);
//                    }
//                }
//                else
//                {
//                    await _next(httpContext);
//                }
//            }
            
//        }
//        private async Task HandleResponse(HttpContext httpContext, string message)
//        {
//            httpContext.Response.ContentType = "application/json";
//            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
//            JObject responseData = new()
//            {
//                ["statusCode"] = httpContext.Response.StatusCode,
//                ["message"] = message
//            };
//            await httpContext.Response.WriteAsync(responseData.ToString());
//        }
//    }

//    // Extension method used to add the middleware to the HTTP request pipeline.
//    public static class UserHandleMiddlewareExtensions
//    {
//        public static IApplicationBuilder UseUserHandleMiddleware(this IApplicationBuilder builder)
//        {
//            return builder.UseMiddleware<UserHandlerMiddleware>();
//        }
//    }
//}
