using SAP_DIAPI_Demo.Infrastructure;
using SAP_DIAPI_Demo.Interfaces.Repository;
using SAP_DIAPI_Demo.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SAP_DIAPI_Demo.Repository
{
    public class ItemRepositoryDI : IItemRepository
    {
        public BaseResponse<bool> UpdatePriceDI(ItemModel priceData)
        {
            var company = SapConnector.GetCompany();
            var vItem = (Items)company.GetBusinessObject(BoObjectTypes.oItems);

            try
            {
                if (!vItem.GetByKey(priceData.ItemCode))
                    return BaseResponse<bool>.Failure(-404, "Item not found.");

                bool CheckPriceListFound = false;
                for (int i = 0; i < vItem.PriceList.Count; i++)
                {
                    vItem.PriceList.SetCurrentLine(i);
                    if (vItem.PriceList.PriceList == priceData.PriceList)
                    {
                        vItem.PriceList.Price = priceData.Price;
                        if (!string.IsNullOrEmpty(priceData.Currency) & priceData.Currency.Length < 3)
                            vItem.PriceList.Currency = priceData.Currency;

                        CheckPriceListFound = true;
                        break;
                    }
                }

                if (!CheckPriceListFound)
                    return BaseResponse<bool>.Failure(-1, $"Price List {priceData.PriceList} not found");

                if (vItem.Update() != 0)
                {
                    company.GetLastError(out int errCode, out string errMsg);
                    return BaseResponse<bool>.Failure(errCode, errMsg);
                }

                return BaseResponse<bool>.Ok(true);
            }
            finally
            {
                if (vItem != null) Marshal.ReleaseComObject(vItem);
            }
        }
    }
}
