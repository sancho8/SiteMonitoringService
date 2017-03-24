using Quartz;
using Quartz.Impl;

namespace SiteMonitoringService.Jobs
{
    class SiteCheckerSheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail googleChecker = JobBuilder.Create<SiteStatusChecker>().Build();
            IJobDetail appleChecker = JobBuilder.Create<SiteStatusChecker>().Build();
            IJobDetail microsoftChecker = JobBuilder.Create<SiteStatusChecker>().Build();

            googleChecker.JobDataMap["url"] = "https://www.google.com"; //for sending in Execute() method in job class
            appleChecker.JobDataMap["url"] = "https://www.apple.com";
            microsoftChecker.JobDataMap["url"] = "https://www.Microsoft.com";

            ITrigger triggerGoogle = TriggerBuilder.Create()  // create trigger
                .WithIdentity("triggerGoogle", "checkerGroup")    
                .StartNow()                            //start now
                .WithSimpleSchedule(x => x            // setting shedule
                    .WithIntervalInMinutes(2)         
                    .RepeatForever())                 
                .Build();                             

            scheduler.ScheduleJob(googleChecker, triggerGoogle);        // begin executing work

            ITrigger triggerApple = TriggerBuilder.Create()  
                .WithIdentity("triggerApple", "checkerGroup")                   
                .WithSimpleSchedule(x => x           
                    .WithIntervalInMinutes(5)         
                    .RepeatForever())                 
                .Build();                             

            scheduler.ScheduleJob(appleChecker, triggerApple);       

            ITrigger triggerMicrosoft = TriggerBuilder.Create() 
                .WithIdentity("triggerMicrosoft", "checkerGroup")      
                .StartAt(DateBuilder.TodayAt(22, 15, 00)) //set specific time for start
                .WithSimpleSchedule(x => x            
                    .WithIntervalInHours(48)        
                    .RepeatForever())                   
                .Build();

            scheduler.ScheduleJob(microsoftChecker, triggerMicrosoft);
           
        }
    }
}
