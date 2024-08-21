//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.WebSockets;
//using System.Threading.Tasks;

//namespace Project.App.Websocket
//{
//    [Route("api/ws")]
//    [ApiController]
//    public class WebSocketController : ControllerBase
//    {
//        private readonly WebSocketHandler _webSocketHandler;

//        public WebSocketController(WebSocketHandler webSocketHandler)
//        {
//            _webSocketHandler = webSocketHandler;
//        }

//        [HttpGet]
//        public async Task Get()
//        {
//            if (HttpContext.WebSockets.IsWebSocketRequest)
//            {
//                WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
//                await _webSocketHandler.HandleWebSocket(HttpContext, webSocket);
//            }
//            else
//            {
//                HttpContext.Response.StatusCode = 400;
//            }
//        }
//    }
//}
