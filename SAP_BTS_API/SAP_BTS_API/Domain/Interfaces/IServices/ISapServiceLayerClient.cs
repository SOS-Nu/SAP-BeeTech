using System.Threading.Tasks;

namespace SAP_BTS_API.Domain.Interfaces.IServices
{
    public interface ISapServiceLayerClient
    {
        Task<T?> GetAsync<T>(string endpoint);

        Task<TResponse?> PostAsJsonAsync<TRequest, TResponse>(string endpoint, TRequest data);

        Task PatchAsJsonAsync<TRequest>(string endpoint, TRequest data);

        Task PutAsJsonAsync<TRequest>(string endpoint, TRequest data);

        Task DeleteAsync(string endpoint);
    }
}