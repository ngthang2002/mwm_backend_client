//using Microsoft.AspNetCore.Builder;
//using Project.Modules.Schedules.Services;
//using Quartz;
//using System;
//using System.Diagnostics;
//using System.Threading.Tasks;

//namespace Project.App.Schedules.Jobs
//{
//    public class UpdateScheduleReport : IJob
//    {
//        IScheduleService ScheduleService;
//        public UpdateScheduleReport(IScheduleService ScheduleService)
//        {
//            this.ScheduleService = ScheduleService;
//        }

//        public async Task Execute(IJobExecutionContext context)
//        {
//            await ScheduleService.EndScheduleReportTW();
//        }
//    }
//}
