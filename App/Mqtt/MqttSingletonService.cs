using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
//using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Project.App.DesignPatterns.Reponsitories;
using Microsoft.EntityFrameworkCore;
using Project.App.Databases;
using StackExchange.Redis;
using Project.App.Definitions;

namespace Project.App.Mqtt
{
    public interface IMqttSingletonService
    {
        Task PingMessage(string topic, string payload, bool retain = true);
        Task PingBytes(string topic, byte[] payload, bool retain = true);
        Task SubscribeNewTopicAsync(string chanel);
        string GetMqttClientId();
        Task MessageReceivedHandler();
    }

    public class MqttSingletonService : IMqttSingletonService
    {
        private readonly IConfiguration Configuration;
        private readonly IMqttClient MqttClient;
        private readonly List<string> topics = new();
        private readonly IRepositoryWrapperMariaDB RepositoryWrapperMariaDB;
        private IDatabase Redis;
        private static string MqttClientId = string.Empty;
        private Queue<MqttApplicationMessage> store = new Queue<MqttApplicationMessage>();
        private static List<string> subscribeTopics = new List<string>();
        private byte Idx;
        public readonly static string TOPIC = "mbs/be";
        private readonly IServiceScopeFactory serviceScopeFactory;

        private async Task<(List<byte> keys, byte pos)> FindKeys()
        {
            List<byte> data = new List<byte>();
            byte pos = 0;
            for (byte i = 0; i < 255; i++)
            {
                if (await Redis.KeyExistsAsync("vms/be/" + i))
                {
                    if (i == this.Idx)
                    {
                        pos = (byte) data.Count;
                    }
                    data.Add(i);
                }
            }
            return (data, pos);
        }

        public MqttSingletonService(IConfiguration configuration, IRepositoryWrapperMariaDB RepositoryWrapperMariaDB, IServiceProvider serviceProvider, IRedisDatabaseProvider redisDatabaseProvider, IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            Redis = redisDatabaseProvider.GetDatabase();
            this.RepositoryWrapperMariaDB = RepositoryWrapperMariaDB;
            Configuration = configuration;
            MqttClient mqttClient = new MqttClient()
            {
                ClientId = "VMS_MTC_20",
                CreatedAt = DateTime.UtcNow,
                LatestTime = DateTime.UtcNow.AddYears(100)
            };
            //Configuration["MQTT:ClientId"] + Guid.NewGuid().ToString()
            MqttClientOptionsBuilder options = new MqttClientOptionsBuilder()
                                 .WithClientId(mqttClient.ClientId)
                                 .WithTcpServer(Configuration["MQTT:Server"], int.Parse(Configuration["MQTT:Port"]))
                                 .WithWillMessage(new MqttApplicationMessage() { Payload = new byte[] { 0, 255 }, Topic = TOPIC }); 
            if (!(string.IsNullOrEmpty(Configuration["MQTT:UserName"]) || string.IsNullOrEmpty(Configuration["MQTT:Password"])))
            {
                options = new MqttClientOptionsBuilder()
                                .WithClientId(mqttClient.ClientId)
                                .WithCredentials(Configuration["MQTT:UserName"], Configuration["MQTT:Password"])
                                .WithTcpServer(Configuration["MQTT:Server"], int.Parse(Configuration["MQTT:Port"]))
                                .WithWillMessage(new MqttApplicationMessage() { Payload = new byte[] { 0, 255 }, Topic = TOPIC });
            }
            topics = Configuration.GetSection("Mqtt:Topic").Get<List<string>>();
            this.MqttClient = this.CreateMqttClient(options, mqttClient);

            //xu ly sub


            IMqttClientOptions _options = options.WithClientId(mqttClient.ClientId).Build();
#if DEBUG
            _options = options.WithClientId(mqttClient.ClientId+"_DEBUG").Build();
#endif
            _ = this.MqttClient.ConnectAsync(_options, CancellationToken.None).GetAwaiter().GetResult();
            //Task.Run(MessageReceivedHandler);
        }

        IMqttClient CreateMqttClient(MqttClientOptionsBuilder builder, MqttClient mqttClient)
        {
            IMqttClient MqttClient = new MqttFactory().CreateMqttClient();
            IMqttClientOptions options = builder.WithClientId(mqttClient.ClientId).Build();
#if DEBUG
            options = builder.WithClientId(mqttClient.ClientId + "_DEBUG").Build();
#endif
            MqttClient.UseDisconnectedHandler(async e =>
            {
                subscribeTopics.Clear();
                Console.WriteLine("MQTTDisconnected: " + DateTime.Now.ToString());
                //Log.Information("### DISCONNECTED FROM SERVER ###");
                await Task.Delay(TimeSpan.FromSeconds(3));
                
                try
                {
                    await MqttClient.ConnectAsync(options, CancellationToken.None);
                }
                catch
                {
                    //Log.Information("### RECONNECTING FAILED ###");
                }
            });
            MqttClient.UseApplicationMessageReceivedHandler(async (e) =>
           {
#if DEBUG
                Console.WriteLine(this.store.Count + "| " + this.Idx + " " + e.ApplicationMessage.Topic);
#endif
               this.store.Enqueue(e.ApplicationMessage);
           });
            MqttClient.UseConnectedHandler(async e =>
            {
                Console.WriteLine("MQTTConnected : " + mqttClient.ClientId + " - Time: " + DateTime.Now.ToString());
                mqttClient.LatestTime = DateTime.UtcNow;
                MqttClientId = mqttClient.ClientId;
                await MqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("h2/s/+").WithExactlyOnceQoS().Build());
                //if (topics==null || topics.Count == 0)
                //{
                //    for (byte i = 0; i < 255; i++)
                //    {
                //        if (!await Redis.KeyExistsAsync("vms/be/" + i))
                //        {
                //            this.Idx = i;
                //            await Redis.StringSetAsync("vms/be/" + i, mqttClient.ClientId, TimeSpan.FromSeconds(3));
                //            Console.WriteLine("Idx: "+ this.Idx + " | "+ mqttClient.ClientId);
                //            break;
                //        }
                //    }
                //    (_, byte pos) = await FindKeys();
                //    if (pos == 0)
                //    {
                //        await MqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("h2/will").WithExactlyOnceQoS().Build());
                //    }
                //    await MqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(TOPIC).WithExactlyOnceQoS().Build());
                //    await Task.Delay(1300);
                //    await MqttClient.PublishAsync(new MqttApplicationMessage() { Topic = TOPIC, Payload = new byte[] { 1, this.Idx } });
                //}
                //else
                //{
                   
                //    foreach (string item in topics)
                //    {
                //        await MqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(item).WithExactlyOnceQoS().Build());
                //    }
                //}
            });
            return MqttClient;
        }

        //private async Task SubscribeTopicAsync(byte[] data)
        //{
        //    if (this.MqttClient==null || data == null || data.Length < 1 || !this.MqttClient.IsConnected)
        //        return;
        //    if (data[0] == 0)
        //    {
        //        await Task.Delay(TimeSpan.FromSeconds(15));
        //    }
        //    else
        //    {
        //        await Task.Delay(TimeSpan.FromSeconds(5));
        //    }

        //    (List<byte> keys, byte pos) = await FindKeys();
        //    List<string> devices = await FindDevices(pos, keys.Count);
        //    await UnsubscribeAsync(subscribeTopics.ToArray()); 
        //    subscribeTopics.Clear();
        //    foreach (var item in devices)
        //    {
        //        await this.SubscribeNewTopicAsync("h2/s/" + item);
        //        await this.SubscribeNewTopicAsync("channels/" + item + "/messages/s");
        //        await this.SubscribeNewTopicAsync("exchange/" + item + "/messages");
        //    }
        //}

        public async Task SubscribeNewTopicAsync(string topic)
        {
            if (MqttClient == null || !MqttClient.IsConnected || string.IsNullOrEmpty(topic))
                return;
            await MqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).WithExactlyOnceQoS().Build());
            subscribeTopics.Add(topic);
        }

        public async Task UnsubscribeAsync(params string[] topics)
        {
            if (MqttClient == null || !MqttClient.IsConnected || topics==null || topics.Length==0)
                return;
            await MqttClient.UnsubscribeAsync(topics);
        }

        //private async Task<List<string>> FindDevices(int page, int totalPage)
        //{
        //    using IServiceScope serviceScope = serviceScopeFactory.CreateScope();
        //    IRepositoryWrapperMariaDB RepositoryWrapperMariaDB = serviceScope.ServiceProvider.GetRequiredService<IRepositoryWrapperMariaDB>();
        //    int count = await RepositoryWrapperMariaDB.Devices.FindByCondition(x => x.DeviceStatus == DeviceStatus.Active && !string.IsNullOrEmpty(x.DeviceCode)).Select(x=>x.DeviceCode).Distinct().CountAsync();
        //    int number = count / Math.Max(totalPage,1);
        //    int delta = count % Math.Max(totalPage, 1);
        //    int skip = page * number;
        //    if (delta != 0 && page == totalPage-1)
        //    {
        //        number += delta;
        //    }
        //    Console.WriteLine("INFO: " + count + "|" + number + "|" + skip+" PAGINATION: "+ page+" | "+ totalPage+" | "+ this.Idx);
        //    List<string> deviceCodes = 0;
        //    return deviceCodes;

        //}

        public async Task MessageReceivedHandler()
        {
            {

                if (this.MqttClient!=null && this.MqttClient.IsConnected)
                {
                   await Redis.StringSetAsync("vms/be/" + this.Idx, this.Idx.ToString(), TimeSpan.FromSeconds(3));
                }
                if (this.store.Count > 0)
                {
                    Console.WriteLine("Count "+this.store.Count + "Time " + DateTime.Now);
                    for (int i = 0; i < 20000; i++)
                    {
                        if (this.store.Count <= 0)
                        {
                            Console.WriteLine("End " + this.store.Count + "Time " + DateTime.Now);
                            break;
                        }
                        MqttApplicationMessage message = this.store.Dequeue();
                        if (message != null)
                        {
                            string[] command = message.Topic.Split('/');
                            if (command.Length == 3 && command[0].Equals("h2") && command[1].Equals("s"))
                            {
                                Redis.StringSet(command[2].GetDeviceKey(), DateTime.UtcNow.ToString(), TimeSpan.FromMinutes(6));
                            }
                        }
                    }
                }
            }
        }
        public async Task PingMessage(string topic, string payload, bool retain = true)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithExactlyOnceQoS()
                .WithRetainFlag(retain)
                .Build();
            await MqttClient.PublishAsync(message, CancellationToken.None);
        }
        public async Task PingBytes(string topic, byte[] payload, bool retain = true)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithExactlyOnceQoS()
                .WithRetainFlag(retain)
                .Build();
            await MqttClient.PublishAsync(message, CancellationToken.None);
        }

        public string GetMqttClientId()
        {
            return MqttClientId;
        }
    }
}
