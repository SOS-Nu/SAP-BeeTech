using SAP_DIAPI_Demo.Configurations;
using SAP_DIAPI_Demo.Interfaces.Services;
using SAP_DIAPI_Demo.Repository;

namespace SAP_DIAPI_Demo.Infrastructure.SAP.Factories
{
    public static class SapRepositoryFactory
    {
        public static IBusinessPartnerRepository CreateBusinessPartnerRepository()
        {
            if (AppSetting.UseServiceLayer)
            {
                return new BusinessPartnerRepositorySL();
            }

            return new BusinessPartnerRepositoryDI();
        }
    }
}