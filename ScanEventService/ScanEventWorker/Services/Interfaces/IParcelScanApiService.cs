using System.Collections.Generic;
using System.Threading.Tasks;
using ScanEventWorker.Dtos;

namespace ScanEventWorker.Services.Interfaces
{
    public interface IParcelScanApiService
    { 
        Task<IList<ParcelScanEventDto>> GetParcelScanEvents(int eventId);
    }
}
