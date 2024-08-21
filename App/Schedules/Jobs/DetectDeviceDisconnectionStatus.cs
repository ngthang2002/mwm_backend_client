//using Project.Modules.Devices.Services;
//using Quartz;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Project.App.Schedules.Jobs
//{
//    public class DetectDeviceDisconnectionStatus : IJob
//    {
//        private readonly IDeviceService DeviceService;
//        public DetectDeviceDisconnectionStatus(IDeviceService ScheduleService)
//        {
//            this.DeviceService = ScheduleService;
//        }

//        public async Task Execute(IJobExecutionContext context)
//        {
//            await DeviceService.DetectDeviceDisconnectionStatus();
//        }
//    }
//}
