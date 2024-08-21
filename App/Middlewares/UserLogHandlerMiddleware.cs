//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.DependencyInjection;
//using Project.App.Databases;
//using StackExchange.Redis;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Security.Claims;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace Project.App.Middlewares
//{
//    public class UserLogHandlerMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly IServiceScopeFactory ServiceScopeFactory;

//        public UserLogHandlerMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
//        {
//            _next = next;
//            ServiceScopeFactory = serviceScopeFactory;
//        }
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
//            var originBody = httpContext.Response.Body;
//            try
//            {
//                if (!this.hasAllowAnonymous(httpContext) && httpContext.User.Identity.IsAuthenticated)
//                {
//                    RouterDefine router = logRouters.FirstOrDefault(x => ((httpContext.Request.Path.Value.ToLower().Contains(x.Path.ToLower()) && httpContext.Request.Method.ToLower() == "put") && (x.Method == "PUT")) 
//                                            || ((httpContext.Request.Path.Value.ToLower().Contains(x.Path.ToLower()) && httpContext.Request.Method.ToLower() == "delete") && (x.Method == "DELETE")) 
//                                            || (httpContext.Request.Path.Value.ToLower().Contains("/api/schedule/add_tw") && (x.Path.ToLower() == "/api/schedule/add_tw"))
//                                            || (httpContext.Request.Path.Value.ToLower() == x.Path.ToLower() && x.Method.Equals(httpContext.Request.Method)));
                    
//                    var memStream = new MemoryStream();
//                    httpContext.Response.Body = memStream;
//                    await _next(httpContext);
//                    memStream.Position = 0;
//                    string responseBody = new StreamReader(memStream).ReadToEnd();
//                    if (router != null && responseBody.Contains("200"))
//                    {
//                        IServiceScope serviceScope = ServiceScopeFactory.CreateScope();
//                        IDatabase Redis = serviceScope.ServiceProvider.GetRequiredService<IRedisDatabaseProvider>().GetDatabase();
//                        router.UserId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
//                        router.Ip = httpContext.Connection.RemoteIpAddress.ToString();
//                        router.ActionDate = DateTime.UtcNow;
//                        router.Detail = responseBody.Replace('\"', ' ').Substring(responseBody.IndexOf(':') + 1, responseBody.IndexOf(',') - responseBody.IndexOf(':') - 1);
//                        string newLog = JsonSerializer.Serialize(router);
//                        await Redis.StringAppendAsync("UserActionLog", "|" + newLog);
//                    }
//                    var memoryStreamModified = new MemoryStream();
//                    var sw = new StreamWriter(memoryStreamModified);
//                    sw.Write(responseBody);
//                    sw.Flush();
//                    memoryStreamModified.Position = 0;

//                    await memoryStreamModified.CopyToAsync(originBody).ConfigureAwait(false);
//                }
//                else
//                {
//                    await _next(httpContext);
//                }
//            }
//            finally
//            {
//                httpContext.Response.Body = originBody;
//            }
//        }

//        List<RouterDefine> logRouters = new List<RouterDefine>
//        {
//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/Schedule/add" , Module = "Schedule"},
//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/Schedule/add_tw" , Module = "Schedule TW"},
//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/Schedule/add/mtc" , Module = "Schedule MTC"},
//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/Schedule/add/radio" , Module = "Schedule RADIO"},
//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/Schedule/add_continue/mtc" , Module = "Schedule MTC"},
//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/Schedule/add_continue/radio" , Module = "Schedule RADIO"},
//            new RouterDefine(){ LogType = UserActionType.Confirm, Method= "POST", Path = "/api/Schedule/confirm" , Module = "Schedule"},
//            new RouterDefine(){ LogType = UserActionType.Confirm, Method= "POST", Path = "/api/Schedule/confirm/mtc" , Module = "Schedule MTC"},
//            new RouterDefine(){ LogType = UserActionType.Confirm, Method= "POST", Path = "/api/Schedule/confirm/radio" , Module = "Schedule RADIO"},
//            new RouterDefine(){ LogType = UserActionType.Cancel, Method= "POST", Path = "/api/Schedule/cancel" , Module = "Schedule"},
//            new RouterDefine(){ LogType = UserActionType.Cancel, Method= "POST", Path = "/api/Schedule/cancel/mtc" , Module = "Schedule MTC"},
//            new RouterDefine(){ LogType = UserActionType.Cancel, Method= "POST", Path = "/api/Schedule/cancel/radio" , Module = "Schedule RADIO"},
//            new RouterDefine(){ LogType = UserActionType.Update, Method= "PUT", Path = "/api/Schedule/update" , Module = "Schedule"},

//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/RadioChannel" , Module = "RadioChannel"},
//            new RouterDefine(){ LogType = UserActionType.Confirm, Method= "POST", Path = "/api/RadioChannel/confirm" , Module = "RadioChannel"},

//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/Playlists" , Module = "Playlists"},
//            new RouterDefine(){ LogType = UserActionType.Delete, Method= "POST", Path = "/api/Playlists/DeleteMany" , Module = "Playlists"},

//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/Medias" , Module = "Medias"},
//            new RouterDefine(){ LogType = UserActionType.Confirm, Method= "POST", Path = "/api/Medias/UpdateStatus" , Module = "Medias"},

//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/Devices" , Module = "Devices"},
//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/Devices/add/radio" , Module = "Devices"},
//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/Devices/add/mtc" , Module = "Devices"},
//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/Devices/add_child/mtc" , Module = "Devices"},
//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/Devices/add_child/radio" , Module = "Devices"},
//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/Devices/add_child/mtc" , Module = "Devices"},
//            new RouterDefine(){ LogType = UserActionType.Update, Method= "PUT", Path = "/api/Devices" , Module = "Devices"},
//            new RouterDefine(){ LogType = UserActionType.Delete, Method= "DELETE", Path = "/api/Devices" , Module = "Devices"},
//            new RouterDefine(){ LogType = UserActionType.Delete, Method= "DELETE", Path = "/api/devices/delete/radio" , Module = "Devices"},
//            new RouterDefine(){ LogType = UserActionType.Delete, Method= "DELETE", Path = "/api/devices/delete/mtc" , Module = "Devices"},

//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/Area" , Module = "Area"},
//            new RouterDefine(){ LogType = UserActionType.Delete, Method= "DELETE", Path = "/api/Area" , Module = "Area"},
//            new RouterDefine(){ LogType = UserActionType.Update, Method= "PUT", Path = "/api/Area" , Module = "Area"},

//            new RouterDefine(){ LogType = UserActionType.Create, Method= "POST", Path = "/api/Accounts" , Module = "Accounts"},
//            new RouterDefine(){ LogType = UserActionType.Delete, Method= "POST", Path = "/api/Accounts/DeleteMany" , Module = "Accounts"},
//            new RouterDefine(){ LogType = UserActionType.Update, Method= "PUT", Path = "/api/Accounts" , Module = "Accounts"},
//        };
//    }

//    // Extension method used to add the middleware to the HTTP request pipeline.
//    public static class UserLogHandlerMiddlewareExtensions
//    {
//        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
//        {
//            return builder.UseMiddleware<UserLogHandlerMiddleware>();
//        }
//    }
//}
