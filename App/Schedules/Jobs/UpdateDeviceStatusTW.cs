//using Microsoft.AspNetCore.Builder;
//using Project.Modules.Devices.Services;
//using Project.Modules.TWComunication.VTC.Services;
//using Quartz;
//using System.Threading.Tasks;

//namespace Project.App.Schedules.Jobs
//{
//    public class UpdateDeviceStatusTW : IJob
//    {
//        IDeviceService DeviceService;
//        ISendTwSevice SendTwSevice;
//        public UpdateDeviceStatusTW(IDeviceService ScheduleService, ISendTwSevice sendTwSevice)
//        {
//            this.DeviceService = ScheduleService;
//            SendTwSevice = sendTwSevice;
//        }

//        public async Task Execute(IJobExecutionContext context)
//        {
//            //await DeviceService.DeviceStatusReportTW();
//            await SendTwSevice.SendDeviceStatus();
//        }
//    }
//}
