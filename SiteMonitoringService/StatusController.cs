﻿using System;
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
        [HttpPost]
        [HttpGet]
        [Route("api/CheckStatus")]
        public bool CheckStatus()
        {
            ServiceController sc = new ServiceController("MyTestService"); //init controller for service
            return (sc.Status == ServiceControllerStatus.Running); //return true if service is running
        }
    }
}
