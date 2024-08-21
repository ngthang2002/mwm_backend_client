using Microsoft.Extensions.DependencyInjection;
using Project.App.DesignPatterns.Reponsitories;
using Project.App.Mqtt;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.App.Schedules.Jobs
{
    public class UpdateLatestTimeMqttClient : IJob
    {
        private IServiceScopeFactory ServiceScopeFactory;
        private IMqttSingletonService MqttSingletonService;

        public UpdateLatestTimeMqttClient(IServiceScopeFactory serviceScopeFactory,IMqttSingletonService mqttSingletonService)
        {
            ServiceScopeFactory = serviceScopeFactory;
            MqttSingletonService = mqttSingletonService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("UpdateLatestTimeMqttClientStart...");
            using IServiceScope serviceScope = ServiceScopeFactory.CreateScope();
            IRepositoryWrapperMariaDB RepositoryWrapperMariaDB = serviceScope.ServiceProvider.GetRequiredService<IRepositoryWrapperMariaDB>();

            string mqttClientId = MqttSingletonService.GetMqttClientId();

            MqttClient mqttClient = RepositoryWrapperMariaDB.MqttClients.FindByCondition(x => x.ClientId.Equals(mqttClientId)).FirstOrDefault();
            if(mqttClientId is not null)
            {
                mqttClient.LatestTime = DateTime.UtcNow;
                RepositoryWrapperMariaDB.MqttClients.Update(mqttClient);
                await RepositoryWrapperMariaDB.SaveChangesAsync();
                Console.WriteLine("UpdateLatestTimeMqttClientUpdate: " + mqttClient.ClientId);
            }
            Console.WriteLine("UpdateLatestTimeMqttClientEnd... " + mqttClientId);
        }
    }
}
