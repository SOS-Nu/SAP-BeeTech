using SAP_Integration.Core.Configurations;
using SAP_Integration.Core.Interfaces.Repository;
using SAP_Integration.Core.Interfaces.Services;
using SAP_Integration.Core.Repositories;
using SAP_Integration.Core.Repository;
using SAP_Integration.Core.Repository.RepositorySL;

namespace SAP_Integration.Core.Infrastructure.SAP.Factories
{
    public static class SapRepositoryFactory
    {

        public static ISalesOrderRepository CreateSalesOrderRepository()
        {
            if (AppSetting.UseServiceLayer)
            {
                return new SalesOrderRepositorySL();
            }

            return new SalesOrderRepositoryDI();
        }
        public static IBusinessPartnerRepository CreateBusinessPartnerRepository()
        {
            if (AppSetting.UseServiceLayer)
            {
                return new BusinessPartnerRepositorySL();
            }

            return new BusinessPartnerRepositoryDI();
        }
        public static IItemRepository CreateItemRepository()
        {
            if (AppSetting.UseServiceLayer)
            {
                return new ItemRepositorySL();
            }

            return new ItemRepositoryDI();
        }

    }
}