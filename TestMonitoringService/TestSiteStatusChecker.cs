using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.NetworkInformation;

namespace TestMonitoringService
{
    [TestClass]
    public class TestSiteStatusChecker
    {
        [TestMethod]
        public void TestImpossibleUrl()
        {
            //arrange
            string url = "";

            //act
            SiteMonitoringService.Jobs.SiteStatusChecker c = new SiteMonitoringService.Jobs.SiteStatusChecker();

            //asserts
            Assert.AreNotEqual(c.checkSite(url), "available");
        }

        [TestMethod]
        public void TestPossibleUrl()
        {
            //arrange
            string url = "https://www.google.com";
            bool isConnectionWorks = false;

            //check connection using ping
            Ping myPing = new Ping();
            String host = "google.com";
            byte[] buffer = new byte[32];
            int timeout = 1000;
            PingOptions pingOptions = new PingOptions();
            PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
            if(reply.Status == IPStatus.Success)
            {
                isConnectionWorks = true;
            }

            //act
            SiteMonitoringService.Jobs.SiteStatusChecker c = new SiteMonitoringService.Jobs.SiteStatusChecker();

            //asserts
            if (isConnectionWorks)
            {
                Assert.AreEqual(c.checkSite(url), "available");
            }
            else
            {
                Assert.AreNotEqual(c.checkSite(url), "available");
            }
        }
    }
}
