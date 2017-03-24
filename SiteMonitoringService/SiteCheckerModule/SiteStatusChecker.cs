using Quartz;
using System;
using System.IO;
using System.Net;

namespace SiteMonitoringService.Jobs
{
    public class SiteStatusChecker : IJob
    {

        private string pathToLogFile = "C:\\sitechecker_log.txt"; //path to log files
        
        void IJob.Execute(IJobExecutionContext context)
        {
            string siteUrl = context.JobDetail.JobDataMap.GetString("url"); //get neccessary url for testing

            //forming logs
            writeLog(String.Format("{0}: Site \"{1}\" was {2}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), siteUrl, checkSite(siteUrl)));
        }

        private void writeLog(string str)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(pathToLogFile, true))
                {
                    writer.WriteLine(str);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                writeLog(str); //if file is open is busy, try one more time
            }
        }

        //retutn status of site(available or unavailable + casue)
        public string checkSite(string url)
        {
            try
            {

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                request.Method = "HEAD";

                HttpWebResponse response = request.GetResponse() as HttpWebResponse; 

                response.Close();

                return "available";
            }
            catch (Exception ex)
            {
                return"unavailable, cause: " + ex.Message; //return also error description
            }
        }
    }
}
