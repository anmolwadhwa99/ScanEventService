using System.ServiceProcess;
using System.Timers;
using ScanEventWorker.Logging.Interfaces;
using ScanEventWorker.Services.Interfaces;

namespace ScanEventWorker
{
    public partial class ParcelEventsService : ServiceBase
    {
        private readonly IParcelService _parcelService;
        private readonly ILogger _logger;
        private readonly Timer _timer;

        public ParcelEventsService(
            IParcelService parcelService,
            ILogger logger
        )
        {
            InitializeComponent();
            _parcelService = parcelService;
            _logger = logger;
            _timer = new Timer();
        }

        protected override void OnStart(string[] args)
        {
            System.Diagnostics.Debugger.Launch();
            _timer.Interval = 1; // runs every 60 seconds
            _timer.Elapsed += OnTimerTick;
            _timer.Enabled = true;
            _timer.Start();
        }
        
        protected override void OnStop()
        {
        }

        private void OnTimerTick(object sender, ElapsedEventArgs args)
        {
           _parcelService.ProcessParcel();
        }
    }
}
