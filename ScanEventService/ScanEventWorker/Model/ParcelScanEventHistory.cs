using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ScanEventWorker.Dtos;
using ScanEventWorker.Model.Enums;

namespace ScanEventWorker.Model
{
    [Table("ParcelScanEventHistory")]
    public class ParcelScanEventHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EventId { get; set; }
        public int ParcelId { get; set; }
        public ParcelType ParcelType { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
        public string StatusCode { get; set; }
        public string RunId { get; set; }

        public ParcelScanEventHistory(ParcelScanEventDto parcelScanEventDto)
        {
            EventId = parcelScanEventDto.EventId;
            ParcelId = parcelScanEventDto.ParcelId;
            ParcelType = parcelScanEventDto.Type;
            CreatedDateTimeUtc = parcelScanEventDto.CreatedDateTimeUtc;
            StatusCode = parcelScanEventDto.StatusCode;
            RunId = parcelScanEventDto.User.RunId;
        }
    }
}
