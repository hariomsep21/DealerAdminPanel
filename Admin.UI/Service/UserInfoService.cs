using Admin.UI.Models;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Admin.UI.Service
{
    public class UserInfoService : IUserInfoService
    {
        private readonly HttpClient _httpClient;

        public UserInfoService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<UserInfoDto> DeleteUserAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7046/api/UserInfoAPI/Delete/{id}");

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
                return JsonConvert.DeserializeObject<UserInfoDto>(responseBody);
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

        public  async Task<UserInfoDto> GetUserByIdAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7046/api/UserInfoAPI/GetUserById{id}");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserInfoDto>(responseBody);
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

        public async Task<IEnumerable<UserInfoDto>> GetUserDetailsAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7046/api/UserInfoAPI/Getdetails");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Use JsonConvert.DeserializeObject directly without try-catch for simplicity
                    return JsonConvert.DeserializeObject<List<UserInfoDto>>(responseBody) ?? new List<UserInfoDto>();
                }
                else
                {
                    throw new HttpRequestException($"Error: {response.StatusCode}");
                }
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

        public async Task<UserInfoDto> UpdateUserAsync(int id, UserInfoDto updatedStateDto)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"https://localhost:7046/api/UserInfoAPI/UpdateId{id}", updatedStateDto);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserInfoDto>(responseBody);
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




























        //public async Task<IEnumerable<UserInfoDto>> GetUserDetailsAsync()
        //{
        //    var response = await _baseService.SendAsync(new RequestDto
        //    {
        //        ApiType = ApiTypeSD.ApiType.GET,
        //        Url = ApiTypeSD.UserInfoAPIBase + "/api/UserInfoAPI/Getdetails"
        //    });

        //    try
        //    {
        //        if (response != null && response.Result != null)
        //        {
        //            var result = JsonConvert.DeserializeObject<List<UserInfoDto>>(response.Result.ToString());
        //            return result ?? new List<UserInfoDto>();
        //        }
        //        else
        //        {
        //            // Log a message or handle the case where response or response.Result is null
        //            // You may return an empty list or throw an exception based on your application logic
        //            return new List<UserInfoDto>();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it as appropriate for your application
        //        // You may want to log the exception and return an error view
        //        return new List<UserInfoDto>();
        //    }
        //}

