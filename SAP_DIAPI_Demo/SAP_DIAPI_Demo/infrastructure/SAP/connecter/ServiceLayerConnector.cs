using Newtonsoft.Json;
using SAP_DIAPI_Demo.Configurations;
using SAP_DIAPI_Demo.domain.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public static class ServiceLayerConnector
{
    private static readonly HttpClient _client;
    private static string _fullCookie = null;

    static ServiceLayerConnector()
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        ServicePointManager.Expect100Continue = false;

        var handler = new HttpClientHandler
        {
            UseCookies = false,
            ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
        };

        _client = new HttpClient(handler)
        {
            BaseAddress = new Uri($"https://{AppSetting.SapServerSL}:50000/b1s/v1/"),
            Timeout = TimeSpan.FromSeconds(30) // FIX treo
        };
    }

    public static async Task<HttpClient> GetClientAsync()
    {
        if (string.IsNullOrEmpty(_fullCookie))
        {
            var loginData = new SapConfig
            {
                CompanyDB = AppSetting.SapCompanyDB,
                UserName = AppSetting.SapUserName,
                Password = AppSetting.SapPassword
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(loginData),
                new UTF8Encoding(false),
                "application/json"
            );

            HttpResponseMessage response;
            try
            {
                response = await _client.PostAsync("Login", content);
            }
            catch (TaskCanceledException)
            {
                throw new Exception("SAP Login timeout");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"SAP connection error: {ex.Message}");
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!(response.StatusCode == HttpStatusCode.BadGateway 
                || response.StatusCode == HttpStatusCode.Unauthorized 
                ||  (int)response.StatusCode == 299) && !response.IsSuccessStatusCode)
            {
                throw new Exception($"SAP Login Failed: {responseBody}");
            }

            
            // Lấy full cookie (B1SESSION + ROUTEID)
            if (response.Headers.TryGetValues("Set-Cookie", out var cookies))
            {

                //// 2. Parse chuỗi JSON thành object
                //var json = Newtonsoft.Json.Linq.JObject.Parse(responseBody);

                //// 3. Lấy SessionId (Lưu ý: SAP thường trả về "SessionId" viết hoa chữ S)
                //string sessionId = json["SessionId"]?.ToString();
                _fullCookie = cookies.FirstOrDefault(c => c.Contains("B1SESSION"));

                _client.DefaultRequestHeaders.Remove("Cookie");
                _client.DefaultRequestHeaders.TryAddWithoutValidation("Cookie", _fullCookie);
            }
        }

        return _client;
    }

    public static void ResetSession()
    {
        _fullCookie = null;
    }


    public static async Task<HttpResponseMessage> ExecuteWithRetryAsync(Func<HttpClient, Task<HttpResponseMessage>> action, int maxRetries = 10)
    {
        int retryCount = 0;
        while (true)
        {
            var client = await GetClientAsync();
            var response = await action(client);

            bool isSessionError = response.StatusCode == HttpStatusCode.Unauthorized ||
                                  response.StatusCode == HttpStatusCode.BadGateway ||
                                  (int)response.StatusCode == 299;

            if (isSessionError && retryCount < maxRetries)
            {
                retryCount++;
                ResetSession(); 
                continue;
            }

            return response;
        }
    }
}
