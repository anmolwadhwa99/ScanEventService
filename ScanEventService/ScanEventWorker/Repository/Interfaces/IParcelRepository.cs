using System.Collections.Generic;
using ScanEventWorker.Model;

namespace ScanEventWorker.Repository.Interfaces
{
    public interface IParcelRepository
    {
        void SaveParcelEvents(IList<ParcelScanEventHistory> scanEvents);
        int GetLastProcessedScanEvent();
        void UpdateLastProcessedScanEvent(int eventId);
    }
}
