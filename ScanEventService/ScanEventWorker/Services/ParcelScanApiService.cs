using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ScanEventWorker.Dtos;
using ScanEventWorker.Logging.Interfaces;
using ScanEventWorker.Services.Interfaces;

namespace ScanEventWorker.Services
{
    public class ParcelScanApiService : IParcelScanApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public ParcelScanApiService(HttpClient httpClient,
            ILogger logger
            )
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        
        public async Task<IList<ParcelScanEventDto>> GetParcelScanEvents(int eventId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"?=FromEventId={eventId}&Limit={Configuration.ScanEventSize}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                
                if (string.IsNullOrWhiteSpace(json))
                {
                    _logger.LogMessage($"Empty response received from API service. Query parameters: FromEventId:{eventId}, Limit{Configuration.ScanEventSize}");
                    return null;
                }
                
                var scanEvent = JsonConvert.DeserializeObject<ScanEventDto>(json);
                return scanEvent?.ScanEvents;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }

            return null;
        }
    }
}
