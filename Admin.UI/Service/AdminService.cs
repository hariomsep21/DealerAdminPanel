using Admin.UI.Models;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Admin.UI.Service
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _httpClient;

        public AdminService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<AdminDto>> GetAdminDetailsAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7213/api/Admin/register");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<AdminDto>>(responseBody);
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
    

    public async Task<AdminDto> LoginAdmin(AdminDto adminDTO)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7213/api/Admin/login", adminDTO);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AdminDto>(responseBody);
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

        public async Task<AdminDto> RegisterAdmin(AdminDto adminDTO)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7213/api/Admin/register", adminDTO);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AdminDto>(responseBody);
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
