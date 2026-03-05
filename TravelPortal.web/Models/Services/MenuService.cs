using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.EDMX;
using TravelPortal.web.Models.Common;
using TravelPortal.web.Models.Interface;

namespace TravelPortal.web.Models.Services
{
    public class MenuService : IMenu
    {
        private readonly db_silviEntities _context;
        public MenuService()
        {
            _context = new db_silviEntities();
        }
        public JsonResponse AddEdit(MenuModel model)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                var obj = _context.tblMaster_Menu.Find(model.ID);
                if (obj != null)
                {
                    obj.MenuName = model.MenuName;
                    obj.URL = model.URL;
                    obj.PageName = model.PageName;
                    obj.Icon = model.Icon;
                    obj.ParentID = string.IsNullOrEmpty(model.ParentID) ? (int?)null : Convert.ToInt32(model.ParentID);
                    obj.Position = string.IsNullOrEmpty(model.Position) ? (int?)null : Convert.ToInt32(model.Position);
                    obj.ControllerName = model.ControllerName;
                    obj.ActionName = model.ActionName;
                    _context.SaveChanges();

                    response.status = 1;
                    response.message = "Menu updated successfully.";
                }
                else
                {
                    tblMaster_Menu newMenu = new tblMaster_Menu();
                    newMenu.MenuName = model.MenuName;
                    newMenu.URL = model.URL;
                    newMenu.PageName = model.PageName;
                    newMenu.Icon = model.Icon;
                    newMenu.ParentID = string.IsNullOrEmpty(model.ParentID) ? (int?)null : Convert.ToInt32(model.ParentID);
                    newMenu.Position = string.IsNullOrEmpty(model.Position) ? (int?)null : Convert.ToInt32(model.Position);
                    newMenu.ControllerName = model.ControllerName;
                    newMenu.ActionName = model.ActionName;
                    _context.tblMaster_Menu.Add(newMenu);
                    _context.SaveChanges();
                    tblManage_MenuRights newMenuRights = new tblManage_MenuRights();
                    newMenuRights.MenuID = newMenu.ID;
                    newMenuRights.Usrno = 1;
                    newMenuRights.IsAdd = true;
                    newMenuRights.IsEdit = true;
                    newMenuRights.IsDelete = true;
                    _context.tblManage_MenuRights.Add(newMenuRights);
                    _context.SaveChanges();
                    response.status = 1;
                    response.message = "Menu added successfully.";
                }
            }
            catch (Exception ex)
            {
                response.status = 0;
                response.message = ex.Message;
            }
            return response;
        }
        public List<MenuModel> GetAll()
        {
            var menuList = (from menu in _context.tblMaster_Menu
                            join parentMenu in _context.tblMaster_Menu on menu.ParentID equals parentMenu.ID into pm
                            from parentMenu in pm.DefaultIfEmpty()
                            select new MenuModel
                            {
                                ID = menu.ID,
                                MenuName = menu.MenuName,
                                URL = menu.URL,
                                PageName = menu.PageName,
                                Icon = menu.Icon,
                                ParentID = menu.ParentID.ToString(),
                                ParentMenuName = parentMenu != null ? parentMenu.MenuName : string.Empty,
                                Position = menu.Position.ToString(),
                                ControllerName = menu.ControllerName,
                                ActionName = menu.ActionName
                            }).ToList();
            return menuList;
        }
        public MenuModel GetById(int id)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id)
        {
            var objMenu = _context.tblMaster_Menu.Find(id);
            if (objMenu != null)
            {
                _context.tblMaster_Menu.Remove(objMenu);
                _context.SaveChanges();
            }
            return true;
        }
        public bool ToggleActive(int id)
        {
            throw new NotImplementedException();
        }

        public List<MenuModel> GetUserMenu(int Usrno)
        {
            var userMenuList =
                (from a in _context.tblMaster_Menu
                 join parent in _context.tblMaster_Menu
                 on a.ParentID equals parent.ID into parentJoin
                 from parent in parentJoin.DefaultIfEmpty()
                 join rights in _context.tblManage_MenuRights.Where(x => x.Usrno == Usrno)
                 on a.ID equals rights.MenuID into rightsJoin
                 from rights in rightsJoin.DefaultIfEmpty()
                 select new MenuModel
                 {
                     // ===== Menu Fields =====
                     ID = a.ID,
                     MenuName = a.MenuName,
                     URL = a.URL,
                     PageName = a.PageName,
                     Icon = a.Icon,
                     ParentID = a.ParentID.ToString(),
                     ParentMenuName = parent != null ? parent.MenuName : null,
                     Position = a.Position.ToString(),
                     ControllerName = a.ControllerName,
                     ActionName = a.ActionName,

                     // ===== Rights Fields (from base class) =====
                     Usrno = Usrno,
                     Ischeck = rights != null ? 1 : 0,
                     IsAdd = rights != null && rights.IsAdd == true ? 1 : 0,
                     IsEdit = rights != null && rights.IsEdit == true ? 1 : 0,
                     IsDelete = rights != null && rights.IsDelete == true ? 1 : 0
                 }).Where(e => e.Ischeck > 0).ToList();
            return userMenuList;
        }
    }
}