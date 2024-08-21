//using Project.Modules.Surveys.Services;
//using Quartz;
//using System.Threading.Tasks;

//namespace Project.App.Schedules.Jobs
//{
//    public class UpdateSurveyStatus : IJob
//    {
//        private readonly ISurveyService SurveyService;
//        public UpdateSurveyStatus(ISurveyService surveyService)
//        {
//            SurveyService = surveyService;
//        }
//        public async Task Execute(IJobExecutionContext context)
//        {
//            await SurveyService.UpdateStatusSurveyAsync();
//        }
//    }
//}
