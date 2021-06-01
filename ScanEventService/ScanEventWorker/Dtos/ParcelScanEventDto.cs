using System;
using ScanEventWorker.Model.Enums;

namespace ScanEventWorker.Dtos
{
    public class ParcelScanEventDto
    {
        public int EventId { get; set; }
        public int ParcelId { get; set; }
        public ParcelType Type { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
        public string StatusCode { get; set; }
        public DeviceDto Device { get; set; }
        public UserDto User { get; set; }
    }
}
