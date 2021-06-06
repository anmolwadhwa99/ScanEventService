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
            _timer.Interval = 60000; // runs every 60 seconds
            _timer.Elapsed += OnTimerTick;
            _timer.Enabled = true;
            _logger.LogMessage("Parcel scan service started");
            _timer.Start();
        }
        
        protected override void OnStop()
        {
            _logger.LogMessage("Parcel scan service stopped");
        }

        private void OnTimerTick(object sender, ElapsedEventArgs args)
        {
            _parcelService.ProcessParcel();
        }
    }
}
