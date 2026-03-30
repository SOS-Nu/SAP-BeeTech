using SAP_DIAPI_Demo.Configurations;
using SAP_DIAPI_Demo.Interfaces.Repository;
using SAP_DIAPI_Demo.Interfaces.Services;
using SAP_DIAPI_Demo.Repositories;
using SAP_DIAPI_Demo.Repository;
using SAP_DIAPI_Demo.Repository.RepositorySL;

namespace SAP_DIAPI_Demo.Infrastructure.SAP.Factories
{
    public static class SapRepositoryFactory
    {

        //public static ISalesOrderRepository() CreateSalesOrderRepository()
        //{
        //    if (AppSetting.UseServiceLayer)
        //    {
        //        return new SalesOrderRepositorySL();
        //    }

        //    return new SalesOrderRepositoryDI();
        //}
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