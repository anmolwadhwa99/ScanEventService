using System.Collections.Generic;

namespace ScanEventWorker.Dtos
{
    public class ScanEventDto
    {
        public IList<ParcelScanEventDto> ScanEvents { get; set; }
    }
}
