//using Project.App.DesignPatterns.Reponsitories;
//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.WebSockets;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Project.App.Websocket
//{
//    public interface IWebSocketManager
//    {
//        //string AddSocket(WebSocket socket, string deviceId);
//        //Task RemoveSocket(string deviceId);
//        //Task SendSync(string deviceId, string message);
//        //Task StoreMessage(string socketid, string message);
//        //Task MessageRecicvedHandler();
//        //Task PingMessage(string AreaId, string message);
//    }
//    public class WebSocketManager : IWebSocketManager
//    {
//        //private readonly ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();
//        //private Queue<(string, string)> store = new Queue<(string, string)>();
//        //private readonly IDirectDeviceService DirectDeviceService;
//        //private readonly IRepositoryWrapperMariaDB RepositoryWrapperMariaDB;

//        //public WebSocketManager(IDirectDeviceService directDeviceService, IRepositoryWrapperMariaDB repositoryWrapperMariaDB)
//        //{
//        //    DirectDeviceService = directDeviceService;
//        //    RepositoryWrapperMariaDB = repositoryWrapperMariaDB;
//        //}

//        //public string AddSocket(WebSocket socket, string deviceId)
//        //{
//        //    _sockets.TryAdd(deviceId, socket);
//        //    return deviceId;
//        //}

//        //public Task RemoveSocket(string deviceId)
//        //{
//        //    _sockets.TryRemove(deviceId, out var _);
//        //    return Task.CompletedTask;
//        //}

//        //public Task SendSync(string deviceId, string message)
//        //{
//        //    WebSocket webSocket = _sockets.FirstOrDefault(x => x.Key.Equals(deviceId)).Value;
//        //    if (webSocket is null)
//        //    {
//        //        return Task.CompletedTask;
//        //    }
//        //    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
//        //    webSocket.SendAsync(new System.ArraySegment<byte>(buffer), WebSocketMessageType.Text, endOfMessage: true, cancellationToken: CancellationToken.None);
//        //    return Task.CompletedTask; ;
//        //}
//        //public Task StoreMessage(string socketid, string message)
//        //{
//        //    this.store.Enqueue((socketid, message));
//        //    return Task.CompletedTask;
//        //}
//        //public async Task MessageRecicvedHandler()
//        //{
//        //    Console.WriteLine(DateTime.Now + "WebSocket Message count: " + store.Count);
//        //    if (this.store.Count <= 0)
//        //    {
//        //        return;
//        //    }
//        //    for (int i = 0; i < 20000; i++)
//        //    {
//        //        if (this.store.Count <= 0)
//        //        {
//        //            break;
//        //        }
//        //        (string key, string message) = this.store.Dequeue();
//        //        try
//        //        {
//        //            await DirectDeviceService.OnMessageHandler(key, message, RepositoryWrapperMariaDB);
//        //        }
//        //        catch (Exception ex)
//        //        {
//        //            Console.WriteLine("Web Socket Device ERROR: " + ex.GetBaseException() + " | " + key);
//        //        }
//        //    }
//        //    return;
//        //}

//        //public async Task PingMessage(string AreaId, string message)
//        //{
//        //    List<string> areas = RepositoryWrapperMariaDB.AreaRelations.FindByCondition(x => x.ParentId.Equals(AreaId)).Select(x => x.ChildId).ToList();
//        //    areas.Add(AreaId);
//        //    List<string> deviceIds = RepositoryWrapperMariaDB.Devices.FindByCondition(x => areas.Contains(x.AreaId)).Select(x => x.DeviceId).ToList();
//        //    if (deviceIds is null || deviceIds.Count == 0)
//        //        return;
//        //    foreach (string device in deviceIds)
//        //    {
//        //        await SendSync(device, message);
//        //    }
//        //}
//    }
//}
