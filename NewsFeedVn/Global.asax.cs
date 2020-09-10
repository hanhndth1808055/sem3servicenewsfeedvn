using NewsFeedVn.service;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NewsFeedVn
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Runbot1Async();
            RunBot2();
        }

        public static async void Runbot1Async()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<Boot1>().Build();

            ITrigger trigger = TriggerBuilder.Create()
               .WithDailyTimeIntervalSchedule
                 (s =>
                    s.WithIntervalInHours(8)
                   .OnEveryDay()
                   .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(1, 0))
                 )
               .Build();

            scheduler.ScheduleJob(job, trigger);
        }
        public static async void RunBot2()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<Boot2>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(8)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(03, 0))
                  )
                .Build();
            scheduler.ScheduleJob(job, trigger);
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        }
    }
}
