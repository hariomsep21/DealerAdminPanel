using Admin.UI.Models;
using Admin.UI.Service.IService;
using Newtonsoft.Json;
using System.Net;

namespace Admin.UI.Service
{
    public class PvaVariantService : IPvaVariantService
    {
        private readonly HttpClient _httpClient;

        public PvaVariantService(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }
        public async Task<PvaVariantDto> AddVariantAsync(PvaVariantDto newStateDto)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7024/api/PvaVariantAPI/AddVariant", newStateDto);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PvaVariantDto>(responseBody);
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

        public async Task<PvaVariantDto> DeleteVariantAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7024/api/PvaVariantAPI/Delete/{id}");

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
                return JsonConvert.DeserializeObject<PvaVariantDto>(responseBody);
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

        public async Task<PvaVariantDto> GetVariantByIdAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7024/api/PvaVariantAPI/GetVariantById{id}");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PvaVariantDto>(responseBody);
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

        public async Task<IEnumerable<PvaVariantDto>> GetVariantDetailsAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7024/api/PvaVariantAPI/GetAllVariant");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<PvaVariantDto>>(responseBody);
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

        public async Task<PvaVariantDto> UpdateVariantAsync(int id, PvaVariantDto updatedStateDto)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"https://localhost:7024/api/PvaVariantAPI/Update{id}", updatedStateDto);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PvaVariantDto>(responseBody);
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
