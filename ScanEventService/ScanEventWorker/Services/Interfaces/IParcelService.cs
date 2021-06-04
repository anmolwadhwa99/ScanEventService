using System.Threading.Tasks;

namespace ScanEventWorker.Services.Interfaces
{
    public interface IParcelService
    {
        Task ProcessParcel();
    }
}
