using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SiteMonitoringService.Jobs
{
    class SiteStatusChecker : IJob
    {

        private string pathToLogFile = "C:\\sitechecker_log.txt";
        
        void IJob.Execute(IJobExecutionContext context)
        {
            string instName = context.JobDetail.JobDataMap.GetString("domain");
            using (StreamWriter writer = new StreamWriter(pathToLogFile, true))
            {
                string checkResult = checkSite("https://google.com");
                writer.WriteLine(String.Format("{0} Сайт {1} был {2}" + instName,
                    DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), "https://google.com", checkResult));
                writer.Flush();
            }
        }

        private string checkSite(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return "available";
            }
            catch (Exception ex)
            {
                //Any exception will returns false.
                return"unavailable, cause: " + ex.Message;
            }
        }
    }
}
