using SAP_Integration.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_Integration.Core.Interfaces.Repository
{
    public interface IItemRepository
    {
        BaseResponse<bool> UpdatePrice(ItemModel priceData);

    }
}
