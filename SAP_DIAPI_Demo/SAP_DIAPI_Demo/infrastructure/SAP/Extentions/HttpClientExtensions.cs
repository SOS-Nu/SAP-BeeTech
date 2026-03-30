using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SAP_DIAPI_Demo.infrastructure.SAP.Extentions
{
   
    public static class HttpClientExtensions
    {
        // thit client khi dùng sẽ bằng client.patchAsync, ko phải client.senassync nữa,
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
            {
                Content = content
            };
            return client.SendAsync(request);
        }
    }
}

