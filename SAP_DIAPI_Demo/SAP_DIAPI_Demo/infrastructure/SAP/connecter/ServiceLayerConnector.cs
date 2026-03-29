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
        //ServicePointManager.Expect100Continue = true;

        var handler = new HttpClientHandler
        {
            UseCookies = false, 
            ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
        };

        _client = new HttpClient(handler)
        {
            BaseAddress = new Uri($"https://{AppSetting.SapServerSL}:50000/b1s/v1/")
        };
    }

    public static async Task<HttpClient> GetClientAsync()
    {
        if (string.IsNullOrEmpty(_fullCookie))
        {
            var loginData = new SapConfig{
                CompanyDB = AppSetting.SapCompanyDB,
                UserName=  AppSetting.SapUserName, 
                Password =AppSetting.SapPassword
            };
            var content = new StringContent(JsonConvert.SerializeObject(loginData), new UTF8Encoding(false), "application/json");

            var response = await _client.PostAsync("Login", content);
            if (!response.IsSuccessStatusCode) throw new Exception("SAP Login Failed");

            // get b1session set cookie
            if (response.Headers.TryGetValues("Set-Cookie", out var cookies))
            {
                _fullCookie = cookies.FirstOrDefault(c => c.Contains("B1SESSION"));
                _client.DefaultRequestHeaders.Remove("Cookie");
                _client.DefaultRequestHeaders.TryAddWithoutValidation("Cookie", _fullCookie);
            }
        }
        return _client;
    }

    public static void ResetSession() => _fullCookie = null;
}