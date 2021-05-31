using System.Collections.Generic;

namespace ScanEventWorker.Dtos
{
    public class ScanEventDto
    {
        public IList<EventDto> ScanEvents { get; set; }
    }
}
