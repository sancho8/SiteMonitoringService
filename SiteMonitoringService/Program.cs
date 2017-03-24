using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SiteMonitoringService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
    {
#if DEBUG
            using (Microsoft.Owin.Hosting.WebApp.Start<Startup>("http://localhost:8000"))
            {
                Console.WriteLine("Сервер запущен. Нажмите любую клавишу для завершения работы...");
                Console.ReadLine();
            }
            new Service1().Start();

#else
     ServiceBase[] ServicesToRun;
      ServicesToRun = new ServiceBase[] 
			{ 
				new Service1() 
			};
      ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
