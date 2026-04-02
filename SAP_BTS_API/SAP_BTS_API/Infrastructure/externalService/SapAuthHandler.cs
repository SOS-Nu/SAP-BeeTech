using Microsoft.Extensions.Options;
using SAP_BTS_API.Domain.Interfaces;
using SAP_BTS_API.Domain.Interfaces.IServices;
using SAP_BTS_API.Domain.models;
using SAP_BTS_API.Infrastructure.persistence.SAP_DTOs.SAPreponses;
using System.Net.Http.Json;
using System.Text.Json;

namespace SAP_BTS_API.Infrastructure.externalService
{
    public class SapAuthHandler : DelegatingHandler
    {
        private readonly ISapSessionStore _store;
        private readonly SapSettings _settings;


        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = null
        };

        public SapAuthHandler(IOptions<SapSettings> options, ISapSessionStore store)
        {
            _settings = options.Value;
            _store = store;

        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(_store.Cookie))
            {
                _store.Cookie = await LoginAsync(cancellationToken);
            }

            request.Headers.Add("Cookie", _store.Cookie);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _store.Cookie = await LoginAsync(cancellationToken);

                request.Headers.Remove("Cookie");
                request.Headers.Add("Cookie", _store.Cookie);

                response = await base.SendAsync(request, cancellationToken);
            }

            return response;
        }

        private async Task<string> LoginAsync(CancellationToken cancellationToken)
        {
            var loginUrl = $"https://{_settings.SapServerSL}:50000/b1s/v1/Login";
            var body = new
            {
                _settings.CompanyDB,
                _settings.UserName,
                _settings.Password
            };

            var loginRequest = new HttpRequestMessage(HttpMethod.Post, loginUrl)
            {
                Content = JsonContent.Create(body, options: _jsonOptions)
            };

            var res = await base.SendAsync(loginRequest, cancellationToken);

            // success thì get cookie
            if (res.IsSuccessStatusCode && res.Headers.TryGetValues("Set-Cookie", out var cookies))
            {
                return string.Join("; ", cookies);
            }

            await EnsureLoginSuccessAsync(res, cancellationToken);

            return string.Empty; 
        }

        private async Task EnsureLoginSuccessAsync(HttpResponseMessage response, CancellationToken ct)
        {
            var errorContent = await response.Content.ReadAsStringAsync(ct);
            object? finalErrorData = errorContent;

            try
            {
                var sapError = JsonSerializer.Deserialize<SapErrorResponse>(errorContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (sapError?.Error != null)
                {
                    finalErrorData = sapError.Error;
                    // Ném SapException để Middleware bắt và format JSON sạch
                    throw new SapException(response.StatusCode, sapError.Error.Message.Value, finalErrorData);
                }
            }
            catch (SapException) { throw; }
            catch { } 

            throw new SapException(response.StatusCode, "SAP Login Failed", finalErrorData);
        }
    }
}