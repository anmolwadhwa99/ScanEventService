using ScanEventWorker.Model.Enums;

namespace ScanEventWorker.Dtos
{
    public class UserDto
    {
        public string UserId { get; set; }
        public CarrierType CarrierId { get; set; }
        public string RunId { get; set; }
    }
}