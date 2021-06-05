using System.Collections.Generic;
using System.Threading.Tasks;
using ScanEventWorker.Model;

namespace ScanEventWorker.Repository.Interfaces
{
    public interface IParcelRepository
    {
        Task SaveParcelEvents(IList<ParcelScanEventHistory> scanEvents);
        Task<int> GetLastProcessedScanEvent();
        Task UpdateLastProcessedScanEvent(int eventId);
    }
}
