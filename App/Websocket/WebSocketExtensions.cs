using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Project.App.Websocket
{
    public static class WebSocketExtensions
    {
        //public static IApplicationBuilder UseWebSocketHandler(this IApplicationBuilder app)
        //{

        //    var webSocketHandler = app.ApplicationServices.GetService<WebSocketHandler>();
        //    return app.Use(async (context, next) =>
        //    {
        //        if (context.WebSockets.IsWebSocketRequest && context.WebSockets.WebSocketRequestedProtocols.Count == 0)
        //        {
        //            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
        //            await webSocketHandler.HandleWebSocket(context, webSocket);
        //        }
        //        else
        //        {
        //            await next();
        //        }
        //    });
        //}
        //public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        //{
        //    services.AddSingleton<IWebSocketManager, WebSocketManager>();
        //    return services;
        //}
    }
}
