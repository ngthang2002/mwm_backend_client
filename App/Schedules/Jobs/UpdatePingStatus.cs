using Project.App.Mqtt;
using Quartz;
using System.Threading.Tasks;

namespace Project.App.Schedules.Jobs
{
    public class UpdatePingStatus : IJob
    {
        IMqttSingletonService MqttSingletonService;
        public UpdatePingStatus(IMqttSingletonService mqttSingletonService)
        {
            MqttSingletonService = mqttSingletonService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            //DeviceService.UpdatePing();
            await MqttSingletonService.MessageReceivedHandler();
        }
    }
}
