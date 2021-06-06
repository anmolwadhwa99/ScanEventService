using System.Collections.Generic;
using Moq;
using ScanEventWorker.Dtos;
using ScanEventWorker.Logging.Interfaces;
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
            _parcelApiServiceMock.Setup(x => x.GetParcelScanEvents(It.IsAny<int>())).ReturnsAsync(new List<ParcelScanEventDto>());
            _parcelRepositoryMock.Setup(x => x.GetLastProcessedScanEvent()).ReturnsAsync(1);

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
            _parcelApiServiceMock.Setup(x => x.GetParcelScanEvents(It.IsAny<int>())).ReturnsAsync((IList<ParcelScanEventDto>)null);
            _parcelRepositoryMock.Setup(x => x.GetLastProcessedScanEvent()).ReturnsAsync(5);
            
            // Act
            parcelService.ProcessParcel();
            
            // Assert
            _parcelRepositoryMock.Verify(x => x.GetLastProcessedScanEvent(), Times.Once);
            _parcelApiServiceMock.Verify(x => x.GetParcelScanEvents(5), Times.Once);
            _loggerMock.Verify(x => x.LogMessage("Getting latest parcel scan messages after event id 5"), Times.Once);
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
