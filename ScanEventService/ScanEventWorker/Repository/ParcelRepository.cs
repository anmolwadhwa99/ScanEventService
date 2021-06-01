using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using ScanEventWorker.Database.Context;
using ScanEventWorker.Model;
using ScanEventWorker.Repository.Interfaces;

namespace ScanEventWorker.Repository
{
    public class ParcelRepository : IParcelRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ParcelRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext ?? new DatabaseContext();
        }
        
        public void SaveParcelEvents(IList<ParcelScanEventHistory> scanEvents)
        {
            _databaseContext.ParcelScanEvents.AddRange(scanEvents);
            _databaseContext.SaveChanges();
        }

        public int GetLastProcessedScanEvent()
        {
            var parcelEventTracker = _databaseContext.ParcelScantEventTracker.FirstOrDefault();
            return parcelEventTracker?.LastEventId ?? 1;
        }

        public void UpdateLastProcessedScanEvent(int eventId)
        {
            var parcelEventTracker = _databaseContext.ParcelScantEventTracker.FirstOrDefault();

            if (parcelEventTracker != null)
            {
                parcelEventTracker.LastEventId = eventId;
                _databaseContext.ParcelScantEventTracker.AddOrUpdate(parcelEventTracker);
                _databaseContext.SaveChanges();
                return;
            }

            parcelEventTracker = new ParcelScanEvent 
            {  
                Id = 1, 
                LastEventId = eventId
            };
            _databaseContext.ParcelScantEventTracker.Add(parcelEventTracker);
            _databaseContext.SaveChanges();
        }
    }
}
