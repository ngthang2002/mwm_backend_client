using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Project.App.Databases
{
    public interface IRedisDatabaseProvider
    {
        IDatabase GetDatabase();
        ConnectionMultiplexer GetConnectionMultiplexer();
    }
    public class RedisDBContext : IRedisDatabaseProvider
    {
        private ConnectionMultiplexer ConnectionMultiplexer;
        private readonly IConfiguration Configuration;
        public static string Prefix = "VMS_MTC";
        public RedisDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IDatabase GetDatabase()
        {
            if (ConnectionMultiplexer == null)
            {
                ConnectionMultiplexer = ConnectionMultiplexer.Connect(Configuration["ConnectionSetting:RedisDBSettings:ConnectionStrings"]);
                //ConnectionMultiplexer.Configure("lkasdf");
                //ConnectionMultiplexer.GetSubscriber().Subscribe($"__key*@0__:*").OnMessage(async channelMessage =>
                //{
                //    await Task.Delay(1000);
                //    Console.WriteLine((string)channelMessage.Message);
                //});

            }
            return ConnectionMultiplexer.GetDatabase();
        }

        public ConnectionMultiplexer GetConnectionMultiplexer()
        {
            if (ConnectionMultiplexer == null)
            {
                ConnectionMultiplexer = ConnectionMultiplexer.Connect(Configuration["ConnectionSetting:RedisDBSettings:ConnectionStrings"]);
            }
            return ConnectionMultiplexer;
        }
    }
    public static class RedisExtensions
    {

        public static async Task SetRecordAsync<T>(this IDatabase database, string key, T data, TimeSpan? expiredTime)
        {
            string jsondata = JsonConvert.SerializeObject(data, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            await database.StringSetAsync(key, jsondata, expiredTime);
        }
        public static async Task<T> GetRecordAsync<T>(this IDatabase database, string key)
        {
            string jsonData = await database.StringGetAsync(key);
            if (jsonData is null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(jsonData); ;
        }
        public static async Task DeleteRecordAsync(this IDatabase database, string key)
        {
            if (key is not null)
            {
                await database.KeyDeleteAsync(key);
            }
        }

        public static async Task<Dictionary<string, string>> GetDataFromDataBaseAsync(this ConnectionMultiplexer connection, IConfiguration configuration, string sPattern = "*", int dbNumber = 0)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            var keys = connection.GetServer(configuration["ConnectionSetting:RedisDBSettings:HostAndPort"]).Keys(dbNumber, pattern: sPattern);
            IDatabase database = connection.GetDatabase(dbNumber);
            foreach (var key in keys)
            {
                data.Add(key, await database.StringGetAsync(key));
            }

            return data;

        }
    }
}