//using System.Net.WebSockets;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Text;

//namespace Project.App.Websocket
//{
//    public class WebSocketHandler
//    {
//        private readonly IWebSocketManager _webSocketManager;
//        private readonly ILogger<WebSocketHandler> _logger;

//        public WebSocketHandler(IWebSocketManager webSocketManager, ILogger<WebSocketHandler> logger)
//        {
//            _webSocketManager = webSocketManager;
//            _logger = logger;
//        }

//        public async Task HandleWebSocket(HttpContext context, WebSocket webSocket)
//        {
//            var socketId = _webSocketManager.AddSocket(webSocket, context.User.Claims.First().Value);
//            _logger.LogInformation($"New WebSocket connection: {socketId}");

//            try
//            {
//                var buffer = new byte[10000];
//                WebSocketReceiveResult result;

//                do
//                {
//                    result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
//                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
//                    _logger.LogInformation($"Received message from {socketId}: {message}");
//                    await _webSocketManager.StoreMessage(socketId,message);
//                    Array.Clear(buffer, 0, buffer.Length);
//                } while (!result.CloseStatus.HasValue);
//            }
//            catch (WebSocketException ex)
//            {
//                _logger.LogError(ex, $"WebSocket error: {socketId}");
//            }
//            finally
//            {
//                _logger.LogInformation($"Close WebSocket connection: {socketId}");
//                await _webSocketManager.RemoveSocket(socketId);
//                await webSocket.CloseAsync(WebSocketCloseStatus.Empty, "", CancellationToken.None);
//            }
//        }
//    }
//}
