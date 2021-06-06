using System;
using System.Collections.Generic;
using Moq;
using ScanEventWorker.Dtos;
using ScanEventWorker.Logging.Interfaces;
using ScanEventWorker.Model;
using ScanEventWorker.Model.Enums;
using ScanEventWorker.Repository.Interfaces;
using ScanEventWorker.Services;
using ScanEventWorker.Services.Interfaces;
using Xunit;

namespace ScanWorkerTests.Tests
{
    public class ParcelServiceTests
    {
        private Mock<IParcelScanApiService> _parcelApiServiceMock;
        private Mock<ILogger> _loggerMock;
        private Mock<IParcelRepository> _parcelRepositoryMock;

        [Fact]
        public void ProcessParcel_NoEventsToProcess_ReturnsSuccessfullyWithNoExceptions()
        {
            // Arrange
            var parcelService = SetupParcelService();
            _parcelRepositoryMock.Setup(x => x.GetLastProcessedScanEvent()).ReturnsAsync(1);
            _parcelApiServiceMock.Setup(x => x.GetParcelScanEvents(1)).ReturnsAsync(new List<ParcelScanEventDto>());
            

            // Act 
            parcelService.ProcessParcel();

            // Assert
            _parcelRepositoryMock.Verify(x => x.GetLastProcessedScanEvent(), Times.Once);
            _parcelApiServiceMock.Verify(x => x.GetParcelScanEvents(1), Times.Once);
            _loggerMock.Verify(x => x.LogMessage("No new scan events to process..."), Times.Once);
        }

        [Fact]
        public void ProcessParcel_NullEventList_ReturnsSuccessfullyWithNoExceptions()
        {
            // Arrange
            var parcelService = SetupParcelService();
            _parcelRepositoryMock.Setup(x => x.GetLastProcessedScanEvent()).ReturnsAsync(5);
            _parcelApiServiceMock.Setup(x => x.GetParcelScanEvents(5)).ReturnsAsync((IList<ParcelScanEventDto>)null);
            
            
            // Act
            parcelService.ProcessParcel();
            
            // Assert
            _parcelRepositoryMock.Verify(x => x.GetLastProcessedScanEvent(), Times.Once);
            _parcelApiServiceMock.Verify(x => x.GetParcelScanEvents(5), Times.Once);
            _loggerMock.Verify(x => x.LogMessage("Getting latest parcel scan messages after event id 5"), Times.Once);
        }

        [Fact]
        public void ProcessParcel_EventsToProcess_ShouldSaveThemToTheDbAndUpdateEventId()
        {
            // Arrange
            var parcelService = SetupParcelService();
            _parcelRepositoryMock.Setup(x => x.GetLastProcessedScanEvent()).ReturnsAsync(9);
            var eventList = new List<ParcelScanEventDto>
            {
                new ParcelScanEventDto
                {
                    EventId = 10,
                    ParcelId = 3423,
                    Device = new DeviceDto
                    {
                        DeviceId = 23423,
                        DeviceTransactionId = 3523
                    },
                    User = new UserDto
                    {
                        CarrierId = CarrierType.NW,
                        RunId = "23423",
                        UserId = "3423"
                    },
                    Type = ParcelType.DELIVERY,
                    StatusCode = string.Empty,
                    CreatedDateTimeUtc = DateTime.UtcNow.AddDays(-5)
                },
                new ParcelScanEventDto
                {
                    EventId = 11,
                    ParcelId = 342423,
                    Device = new DeviceDto
                    {
                        DeviceId = 6565,
                        DeviceTransactionId = 4323
                    },
                    User = new UserDto
                    {
                        CarrierId = CarrierType.PH,
                        RunId = "325543",
                        UserId = "36765"
                    },
                    Type = ParcelType.PICKUP,
                    StatusCode = string.Empty,
                    CreatedDateTimeUtc = DateTime.UtcNow.AddDays(-5)
                }
            };
            
            _parcelApiServiceMock.Setup(x => x.GetParcelScanEvents(9)).ReturnsAsync(eventList);
            
            // Act
            parcelService.ProcessParcel();
            
            // Assert
            _loggerMock.Verify(x => x.LogMessage("Received 2 scan events. Processing them now ..."), Times.Once);
            _parcelRepositoryMock.Verify(x => x.SaveParcelEvents(It.IsAny<IList<ParcelScanEventHistory>>()), Times.Once);
            _parcelRepositoryMock.Verify(x => x.UpdateLastProcessedScanEvent(11), Times.Once);
        }
        
        private ParcelService SetupParcelService()
        {
            _parcelApiServiceMock = new Mock<IParcelScanApiService>();
            _loggerMock = new Mock<ILogger>();
            _parcelRepositoryMock = new Mock<IParcelRepository>();

            return new ParcelService(_parcelRepositoryMock.Object, _parcelApiServiceMock.Object, _loggerMock.Object);
        }
    }
}
