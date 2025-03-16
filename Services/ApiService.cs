// File: ApiTestWeb/Services/ApiService.cs
using ApiTestWeb.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net;
using Serilog;

namespace ApiTestWeb.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public ApiService()
        {
            HttpClientHandler handler = new HttpClientHandler { UseDefaultCredentials = true };
            _httpClient = new HttpClient(handler);
            _httpClient.BaseAddress = new Uri("https://localhost:44327/"); // Update with the correct API base URL and port
            _logger = Log.ForContext<ApiService>();
        }

        public async Task<string> GetDataAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/values"); // Replace with your actual endpoint

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _logger.Warning("Unauthorized access - 401 error.");
                    throw new UnauthorizedAccessException("Unauthorized access.");
                }
                else
                {
                    _logger.Error($"API request failed with status code: {response.StatusCode}");
                    throw new HttpRequestException($"API request failed with status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.Error(ex, "An error occurred while sending the request.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An unexpected error occurred.");
                throw;
            }
        }
    }
}
