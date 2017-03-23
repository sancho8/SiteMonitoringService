using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SiteMonitoringService
{
    class StatusController: ApiController
    {
        public bool CheckStatus()
        {
            ServiceController sc = new ServiceController("MyTestService");
            return (sc.Status == ServiceControllerStatus.Running);
        }
    }
}
