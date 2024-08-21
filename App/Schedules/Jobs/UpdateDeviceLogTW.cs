//using Project.Modules.TWComunication.VTC.Services;
//using Quartz;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Project.App.Schedules.Jobs
//{
//    public class UpdateDeviceLogTW : IJob
//    {
//        ISendTwSevice SendTwSevice;
//        public UpdateDeviceLogTW(ISendTwSevice sendTwSevice)
//        {
//            SendTwSevice = sendTwSevice;
//        }

//        public async Task Execute(IJobExecutionContext context)
//        {
//            //await DeviceService.DeviceStatusReportTW();
//            await SendTwSevice.SendDeviceLogBySchedule();
//        }
//    }
//}
