using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ScanEventWorker.Dtos;
using ScanEventWorker.Model;
using ScanEventWorker.Repository.Interfaces;
using ScanEventWorker.Services.Interfaces;

namespace ScanEventWorker.Services
{
    public class ParcelService : IParcelService
    {
        private readonly IParcelRepository _parcelRepository;
        
        public ParcelService(IParcelRepository parcelRepository)
        {
            _parcelRepository = parcelRepository;
        }
        
        public void ProcessParcel()
        {
            var json = @"
            {
            ""ScanEvents"":[
      {
         ""EventId"":83269,
         ""ParcelId"":5002,
         ""Type"": ""PICKUP"",
         ""CreatedDateTimeUtc"":""2021-05-11T21:11:34.1506147Z"",
         ""StatusCode"":"""",
         ""Device"":{
            ""DeviceTransactionId"":83269,
            ""DeviceId"":103
         },
         ""User"":{
            ""UserId"":""NC1001"",
            ""CarrierId"":""NC"",
            ""RunId"":""100""
         }
      }
   ]
}";

            var eventsDto = JsonConvert.DeserializeObject<ScanEventDto>(json);
            var scanEvents = new List<ParcelScanEventHistory>();
            var lastEventId = _parcelRepository.GetLastProcessedScanEvent();

            foreach (var eventDto in eventsDto.ScanEvents)
            {
                scanEvents.Add(new ParcelScanEventHistory(eventDto));
            }
            
            _parcelRepository.SaveParcelEvents(scanEvents);
            _parcelRepository.UpdateLastProcessedScanEvent(eventsDto.ScanEvents.Max(x => x.EventId));
        }
    }
}
