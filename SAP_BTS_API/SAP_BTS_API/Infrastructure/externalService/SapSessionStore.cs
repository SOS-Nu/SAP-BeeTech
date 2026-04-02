using SAP_BTS_API.Domain.Interfaces;

namespace SAP_BTS_API.Infrastructure.externalService
{
    public class SapSessionStore : ISapSessionStore
    {
        public string Cookie { get; set; }
    }
}
