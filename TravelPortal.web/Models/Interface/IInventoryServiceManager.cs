using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.web.Models.Common;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Models.Interface
{
    public interface IInventoryServiceManager
    {
        JsonResponse BecomeSupplier(BecomeSupplierModel model);
        JsonResponse AddEditInventoryService(FlightInventoryServiceModel model);
        List<InventoryListViewModel> ListInventory(SearchModel model);
    }
}