using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScanEventWorker.Logging.Interfaces;
using ScanEventWorker.Model;
using ScanEventWorker.Repository.Interfaces;
using ScanEventWorker.Services.Interfaces;

namespace ScanEventWorker.Services
{
    public class ParcelService : IParcelService
    {
        private readonly IParcelScanApiService _parcelScanApiService;
        private readonly IParcelRepository _parcelRepository;
        private readonly ILogger _logger;

        public ParcelService(IParcelRepository parcelRepository,
            IParcelScanApiService parcelScanApiService,
            ILogger logger
            )
        {
            _parcelRepository = parcelRepository;
            _parcelScanApiService = parcelScanApiService;
            _logger = logger;
        }
        
        public async Task ProcessParcel()
        {
            try
            {
                var lastEventId = _parcelRepository.GetLastProcessedScanEvent();
                _logger.LogMessage($"Getting latest parcel scan messages after event id {lastEventId}");

                var events = await _parcelScanApiService.GetParcelScanEvents(lastEventId);
                
                if (events == null)
                {
                    return;
                }
                if (events.Count == 0)
                {
                    _logger.LogMessage("No new scan events to process...");
                    return;
                }

                _logger.LogMessage($"Received {events.Count} scan events. Processing them now ...");
                var scanEvents = new List<ParcelScanEventHistory>();    
                
                foreach (var eventDto in events)
                {
                    scanEvents.Add(new ParcelScanEventHistory(eventDto));
                }

                _parcelRepository.SaveParcelEvents(scanEvents);
                _parcelRepository.UpdateLastProcessedScanEvent(events.Max(x => x.EventId));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }
        }
    }
}
