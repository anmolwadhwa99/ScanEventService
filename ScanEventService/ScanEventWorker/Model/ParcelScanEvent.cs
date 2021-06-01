using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScanEventWorker.Model
{
    [Table("ParcelScanEvent")]
    public class ParcelScanEvent
    {
        [Key]
        public int Id { get; set; }
        
        public int LastEventId { get; set; }
    }
}
