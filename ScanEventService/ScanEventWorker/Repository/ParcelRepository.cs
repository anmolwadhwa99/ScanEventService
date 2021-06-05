using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
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
        
        public async Task SaveParcelEvents(IList<ParcelScanEventHistory> scanEvents)
        {
            _databaseContext.ParcelScanEvents.AddRange(scanEvents);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<int> GetLastProcessedScanEvent()
        {
            var parcelEventTracker = await _databaseContext.ParcelScantEventTracker.FirstOrDefaultAsync();
            return parcelEventTracker?.LastEventId ?? 1;
        }

        public async Task UpdateLastProcessedScanEvent(int eventId)
        {
            var parcelEventTracker = await _databaseContext.ParcelScantEventTracker.FirstOrDefaultAsync();

            if (parcelEventTracker != null)
            {
                parcelEventTracker.LastEventId = eventId;
                _databaseContext.ParcelScantEventTracker.AddOrUpdate(parcelEventTracker);
               await _databaseContext.SaveChangesAsync();
                return;
            }

            parcelEventTracker = new ParcelScanEvent 
            {  
                Id = 1, 
                LastEventId = eventId
            };
            _databaseContext.ParcelScantEventTracker.Add(parcelEventTracker);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
