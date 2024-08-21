//using Project.Modules.Accounts.Services;
//using Quartz;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Project.App.Schedules.Jobs
//{
//    public class UpdateUserActionLog : IJob
//    {
//        IAccountService AccountService;
//        public UpdateUserActionLog(IAccountService AccountService)
//        {
//            this.AccountService = AccountService;
//        }

//        public async Task Execute(IJobExecutionContext context)
//        {
//            await AccountService.UpdateUserLog();
//        }
//    }
//}
