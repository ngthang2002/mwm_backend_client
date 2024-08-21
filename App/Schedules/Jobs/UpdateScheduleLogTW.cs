//using Project.Modules.TWComunication.VTC.Services;
//using Quartz;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Project.App.Schedules.Jobs
//{
//    public class UpdateScheduleLogTW : IJob
//    {
//        ISendTwSevice SendTwSevice;
//        public UpdateScheduleLogTW(ISendTwSevice sendTwSevice)
//        {
//            SendTwSevice = sendTwSevice;
//        }

//        public async Task Execute(IJobExecutionContext context)
//        {
//            //await DeviceService.DeviceStatusReportTW();
//            await SendTwSevice.SendScheduleLogBySchedule();
//        }
//    }
//}
