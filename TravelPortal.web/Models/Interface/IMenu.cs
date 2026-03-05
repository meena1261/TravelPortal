using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.web.Models.Common;

namespace TravelPortal.web.Models.Interface
{
    public interface IMenu
    {
        List<MenuModel> GetAll();
        MenuModel GetById(int id);
        JsonResponse AddEdit(MenuModel model);
        bool Delete(int id);
        bool ToggleActive(int id);
        List<MenuModel> GetUserMenu(int Usrno);
    }
}