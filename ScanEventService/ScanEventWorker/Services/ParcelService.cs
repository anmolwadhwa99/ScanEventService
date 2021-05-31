using System.Collections.Generic;
using Newtonsoft.Json;
using ScanEventWorker.Dtos;
using ScanEventWorker.Services.Interfaces;

namespace ScanEventWorker.Services
{
    public class ParcelService : IParcelService
    {
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
            
            var scanEvents = JsonConvert.DeserializeObject<ScanEventDto>(json);
            var count = scanEvents.ScanEvents.Count;


        }
    }
}