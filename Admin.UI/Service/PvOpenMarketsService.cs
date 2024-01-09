using Admin.UI.Models;
using Admin.UI.Service.IService;
using Newtonsoft.Json;
using System.Net;

namespace Admin.UI.Service
{
    public class PvOpenMarketsService : IPvOpenMarketsService
    {
        private readonly HttpClient _httpClient;

        public PvOpenMarketsService(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }

        public async Task<PvOpenMarketsDto> DeleteOpenMarketAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7024/api/PvOpenMarketsAPI/Delete/{id}");

                // Check if the response indicates a failure (non-success status code)
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        // Handle 404 Not Found
                        // You can return null or throw a custom exception, or handle it according to your application's logic
                        return null;
                    }

                    // Handle other non-success status codes if needed
                    // For example, you might throw a custom exception with details from the response
                    throw new HttpRequestException($"HTTP request error: {response.StatusCode} - {response.ReasonPhrase}");
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PvOpenMarketsDto>(responseBody);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"HTTP request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        public async Task<PvOpenMarketsDto> GetOpenMarketByIdAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7024/api/PvOpenMarketsAPI/GetStateById{id}");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PvOpenMarketsDto>(responseBody);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"HTTP request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        public async Task<IEnumerable<PvOpenMarketsDto>> GetOpenMarketDetailsAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7024/api/PvOpenMarketsAPI/GetAllAggregators");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<PvOpenMarketsDto>>(responseBody);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"HTTP request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }
    }
}
