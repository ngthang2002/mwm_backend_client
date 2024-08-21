//using Microsoft.AspNetCore.Builder;
//using Project.Modules.ExternalDataConvert.ExternalServices;
//using Project.Modules.Schedules.Services;
//using Quartz;
//using System;
//using System.Diagnostics;
//using System.Threading.Tasks;

//namespace Project.App.Schedules.Jobs
//{
//    public class UpdateScheduleStatus : IJob
//    {
//        IScheduleService ScheduleService;
//        IHTTTScheduleService HTTTScheduleService;
//        IHTTTDeviceService HTTTDeviceService;
//        public UpdateScheduleStatus(IScheduleService ScheduleService, IHTTTScheduleService hTTTScheduleService, IHTTTDeviceService hTTTDeviceService)
//        {
//            this.ScheduleService = ScheduleService;
//            this.HTTTScheduleService = hTTTScheduleService;
//            HTTTDeviceService = hTTTDeviceService;
//        }

//        public async Task Execute(IJobExecutionContext context)
//        {
//            await ScheduleService.UpdateStatusJob();
//            await HTTTScheduleService.ProcessIndirectSchedule();
//            await HTTTDeviceService.ProcessIndirectDevice();
//        }
//    }
//}
