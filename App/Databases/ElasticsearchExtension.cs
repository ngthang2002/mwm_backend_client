using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

namespace Project.App.Databases
{
    public static class ElasticsearchExtension
    {
        private const string TablePrefix = "vms";
        public static void AddElasticsearch(
        this IServiceCollection services, IConfiguration configuration)
        {
            string[] nodes = configuration.GetSection($"ConnectionSetting:ElasticsearchSettings:Nodes").Get<string[]>();
            ConnectionSettings connectionSettings;
            if (nodes is null || string.IsNullOrEmpty(nodes[0]))
            {
                return;
            }
            if (nodes.Length == 1)
            {
                #region Connecting to a single node
                Uri uri = new(nodes[0]);
                connectionSettings = new(uri);
                #endregion
            }
            else
            {
                #region Connecting to multiple nodes using a connection pool
                Uri[] uris = new Uri[nodes.Length];
                for (int i = 0; i < nodes.Length; i++)
                {
                    uris[i] = new Uri(nodes[i]);
                }
                StaticConnectionPool staticConnectionPool = new(uris);
                connectionSettings = new(staticConnectionPool);
                #endregion
            }
            connectionSettings.ThrowExceptions(alwaysThrow: true);
            if (!string.IsNullOrEmpty(configuration["ConnectionSetting:ElasticsearchSettings:Username"]) && !string.IsNullOrEmpty(configuration["ConnectionSetting:ElasticsearchSettings:Password"]))
            {
                connectionSettings.BasicAuthentication(configuration["ConnectionSetting:ElasticsearchSettings:Username"], configuration["ConnectionSetting:ElasticsearchSettings:Password"]);
            }
            connectionSettings.RequestTimeout(TimeSpan.FromMinutes(10));

            //connectionSettings
            //    .DefaultMappingFor<DeviceLog>(m => m.IndexName(TablePrefix + "_device_log"))
            //    .DefaultMappingFor<DevicePlayLog>(m => m.IndexName(TablePrefix + "_device_play_log"))
            //    .DefaultMappingFor<AccountActionLog>(m => m.IndexName(TablePrefix + "_action_log"))
            //    .DefaultMappingFor<HTTTScheduleLog>(m => m.IndexName(TablePrefix + "_httt_schedule_diary"))
            //    .DefaultMappingFor<HTTTDeviceScheduleDiary>(m => m.IndexName(TablePrefix + "_httt_device_schedule_diary"))
            //    .DefaultMappingFor<HTTTDeviceGeneric>(m => m.IndexName(TablePrefix + "_httt_device"))
            //    .DefaultMappingFor<HTTTSubSchedule>(m => m.IndexName(TablePrefix + "_httt_sub_schedule"))
            //    .DefaultMappingFor<HTTTStatus>(m => m.IndexName(TablePrefix + "_httt_status"))
            //    .DefaultMappingFor<DeviceStatusHistory>(m => m.IndexName(TablePrefix + "_device_history"))
            //    .DefaultMappingFor<ScheduleLog>(m => m.IndexName(TablePrefix + "_schedule_tw_log"))

                //.DefaultMappingFor<HTTTStatus>(m => m.IndexName(TablePrefix + "_httt_status"))
                //.DefaultMappingFor<HTTTStatus>(m => m.IndexName(TablePrefix + "_httt_status"))
                //.DefaultMappingFor<HTTTStatus>(m => m.IndexName(TablePrefix + "_httt_status"))
                //.DefaultMappingFor<HTTTStatus>(m => m.IndexName(TablePrefix + "_httt_status"))
                ;

            ElasticClient elasticClient = new(connectionSettings);

            //if (!elasticClient.Indices.Exists(TablePrefix + "_device_log").Exists)
            //{
            //    elasticClient.Indices.Create(TablePrefix + "_device_log", creator => creator.Map<DeviceLog>(type => type.AutoMap()));
            //}
            //if (!elasticClient.Indices.Exists(TablePrefix + "_httt_device").Exists)
            //{
            //    elasticClient.Indices.Create(TablePrefix + "_httt_device", creator => creator.Map<HTTTDeviceGeneric>(type => type.AutoMap()));
            //}
            //else
            //{
            //    elasticClient.Map<HTTTDeviceGeneric>(m => m.Properties(ps => ps.Date(s => s.Name(n => n.NgayTao))));
            //}
            //if (!elasticClient.Indices.Exists(TablePrefix + "_httt_sub_schedule").Exists)
            //{
            //    elasticClient.Indices.Create(TablePrefix + "_httt_sub_schedule", creator => creator.Map<HTTTSubSchedule>(type => type.AutoMap()));
            //}

            //if (!elasticClient.Indices.Exists(TablePrefix + "_device_play_log").Exists)
            //{
            //    elasticClient.Indices.Create(TablePrefix + "_device_play_log", creator => creator.Map<DevicePlayLog>(type => type.AutoMap()));
            //}

            //if (!elasticClient.Indices.Exists(TablePrefix + "_action_log").Exists)
            //{
            //    elasticClient.Indices.Create(TablePrefix + "_action_log", creator => creator.Map<AccountActionLog>(type => type.AutoMap()));
            //}

            //if (!elasticClient.Indices.Exists(TablePrefix + "_httt_schedule_diary").Exists)
            //{
            //    elasticClient.Indices.Create(TablePrefix + "_httt_schedule_diary", creator => creator.Map<HTTTScheduleLog>(type => type.AutoMap()));
            //}

            //if (!elasticClient.Indices.Exists(TablePrefix + "_httt_device_schedule_diary").Exists)
            //{
            //    elasticClient.Indices.Create(TablePrefix + "_httt_device_schedule_diary", creator => creator.Map<HTTTDeviceScheduleDiary>(type => type.AutoMap()));
            //}

            //if (!elasticClient.Indices.Exists(TablePrefix + "_httt_status").Exists)
            //{
            //    elasticClient.Indices.Create(TablePrefix + "_httt_status", creator => creator.Map<HTTTStatus>(type => type.AutoMap()));
            //}

            //if (!elasticClient.Indices.Exists(TablePrefix + "_device_history").Exists)
            //{
            //    elasticClient.Indices.Create(TablePrefix + "_device_history", creator => creator.Map<DeviceStatusHistory>(type => type.AutoMap()));
            //}
            //if (!elasticClient.Indices.Exists(TablePrefix + "_schedule_tw_log").Exists)
            //{
            //    elasticClient.Indices.Create((TablePrefix + "_schedule_tw_log"), creator => creator.Map<ScheduleLog>(type => type.AutoMap()));
            //}
            services.AddSingleton<IElasticClient>(elasticClient);
        }

        public static SortDescriptor<T> SortDescriptor<T>(this IElasticClient client, string orderQuerry) where T : class
        {
            var sortDescriptor = new SortDescriptor<T>();
            //sortDescriptor.Field("userName.keyword", Nest.SortOrder.Ascending);
            return sortDescriptor;
        }
    }
}
