//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;
//using Project.App.DesignPatterns.Reponsitories;
//using Project.Modules.Devices.DirectDevice.Services;
//using Project.Modules.Devices.DirectDevice.WebSocket.Services;
//using Project.Modules.Devices.Jira.V2.Services;
//using Project.Modules.Devices.Jira.V3.Services;
//using Project.Modules.Devices.Mira.Services;
//using Project.Modules.Devices.Services;
//using Project.Modules.Devices.Vendor.Services;
//using Quartz;
//using System;
//using System.Threading.Tasks;

//namespace Project.App.Schedules.Jobs
//{
//    public class UpdateLogStatus : IJob
//    {
//        private readonly IMiraSubcribeServiceV3 MiraSubcribeServiceV3;
//        private readonly IMiraSubcribeServiceV2 MiraSubcribeServiceV2;
//        private readonly IDeviceService DeviceService;
//        private readonly IMiraSubcribeService MiraSubcribeService;
//        private readonly IServiceScopeFactory serviceScopeFactory;
//        private readonly IVendorSubscribeService VendorSubscribeService;
//        private readonly IDirectDeviceService DirectDeviceService;

//        public UpdateLogStatus(IMiraSubcribeServiceV3 MiraSubcribeServiceV3, IMiraSubcribeServiceV2 MiraSubcribeServiceV2, IMiraSubcribeService MiraSubcribeService, IDeviceService ScheduleService, IServiceScopeFactory serviceScopeFactory, IDirectDeviceService directDeviceService, IVendorSubscribeService vendorSubscribeService)
//        {
//            this.DeviceService = ScheduleService;
//            this.MiraSubcribeServiceV3 = MiraSubcribeServiceV3;
//            this.MiraSubcribeServiceV2 = MiraSubcribeServiceV2;
//            this.MiraSubcribeService = MiraSubcribeService;
//            this.serviceScopeFactory = serviceScopeFactory;
//            this.VendorSubscribeService = vendorSubscribeService;
//            this.DirectDeviceService = directDeviceService;
//        }

//        public async Task Execute(IJobExecutionContext context)
//        {
//            //await DeviceService.UpdateLog();
//            try
//            {
//                using IServiceScope serviceScope = serviceScopeFactory.CreateScope();
//                IRepositoryWrapperMariaDB RepositoryWrapperMariaDB = serviceScope.ServiceProvider.GetRequiredService<IRepositoryWrapperMariaDB>();
//                await VendorSubscribeService.ProcessLogs(RepositoryWrapperMariaDB);
//                await MiraSubcribeServiceV2.ProcessLogs(RepositoryWrapperMariaDB);
//                await MiraSubcribeServiceV3.ProcessLogs(RepositoryWrapperMariaDB);
//                await MiraSubcribeService.ProcessDatas(RepositoryWrapperMariaDB);
//                await DeviceService.UpdatePlayStatus(RepositoryWrapperMariaDB);
//                await DirectDeviceService.ProccessRadioLog(RepositoryWrapperMariaDB);
//                await DirectDeviceService.ProccessMTCLog(RepositoryWrapperMariaDB);

//            }
//            catch (Exception ex)
//            {

//                Console.WriteLine("Execute: " + ex.GetBaseException());
//            }
//        }
//    }
//}
