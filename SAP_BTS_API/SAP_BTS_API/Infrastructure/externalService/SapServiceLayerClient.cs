using global::SAP_BTS_API.Domain.Interfaces.IServices;
using SAP_BTS_API.Infrastructure.persistence.SAP_DTOs.SAPreponses;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SAP_BTS_API.Infrastructure.ExternalServices.Sap
{
    public class SapServiceLayerClient : ISapServiceLayerClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public SapServiceLayerClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null, 
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            // Tự động deserialize JSON từ SAP thành Object C#
            var response = await _httpClient.GetAsync(endpoint);
            await EnsureSuccessAsync(response);
            return await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
        }

        public async Task<TResponse?> PostAsJsonAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data, _jsonOptions);
            await EnsureSuccessAsync(response);
            return await response.Content.ReadFromJsonAsync<TResponse>(_jsonOptions);
        }

        public async Task PatchAsJsonAsync<TRequest>(string endpoint, TRequest data)
        {
            var response = await _httpClient.PatchAsJsonAsync(endpoint, data, _jsonOptions);
            await EnsureSuccessAsync(response);
        }

        public async Task PutAsJsonAsync<TRequest>(string endpoint, TRequest data)
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, data, _jsonOptions);
            await EnsureSuccessAsync(response);
        }

        public async Task DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            await EnsureSuccessAsync(response);
        }

        private async Task EnsureSuccessAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                object? finalErrorData = errorContent; 

                try
                {
                    var sapError = JsonSerializer.Deserialize<SapErrorResponse>(errorContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (sapError?.Error != null)
                    {
                        finalErrorData = sapError.Error;

                        throw new SapException(response.StatusCode, sapError.Error.Message.Value, finalErrorData);
                    }
                }
                catch { }

                throw new SapException(response.StatusCode, "SAP Error", finalErrorData);
            }
        }
    }
}