using System.ServiceProcess;


namespace SiteMonitoringService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        public void Start()
        {
            Jobs.SiteCheckerSheduler.Start();
        }
    }
}
