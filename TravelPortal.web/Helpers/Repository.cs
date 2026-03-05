using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelPortal.EDMX;
using TravelPortal.web.Models;
using TravelPortal.web.Models.Common;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Helpers
{
    public class Repository
    {
        public db_silviEntities db;
        public Repository()
        {
            db = new db_silviEntities();
        }

        public JsonResponse AddEditUserTypes(UserTypeModel model)
        {
            JsonResponse response = new JsonResponse();
            var objUserType = db.tblMaster_UserType.Where(x => x.ID == model.ID).FirstOrDefault();
            if (objUserType == null)
            {
                objUserType = new tblMaster_UserType();
                objUserType.UserType = model.UserType;
                objUserType.CreatedByUsrno = model.CreatedByUsrno;
                db.tblMaster_UserType.Add(objUserType);
                response.status = 1;
                response.message = "Successfully Added";
            }
            else
            {
                objUserType.UserType = model.UserType;
                response.status = 1;
                response.message = "Successfully Updated";
            }
            db.SaveChanges();
            return response;
        }
        public List<UserTypeModel> ListUserTypes(int Usrno = 0)
        {
            List<UserTypeModel> models = new List<UserTypeModel>();
            var objUserTypes = db.tblMaster_UserType.ToList();
            foreach (var item in objUserTypes)
            {
                UserTypeModel model = new UserTypeModel();
                model.ID = item.ID;
                model.UserType = item.UserType;
                model.CreatedByUsrno = (int)item.CreatedByUsrno;
                models.Add(model);
            }
            return models;
        }
        public List<MenuModel> ListMenu(int Usrno = 0)
        {
            var query =
                from a in db.tblMaster_Menu
                join parent in db.tblMaster_Menu
                on a.ParentID equals parent.ID into parentJoin
                from parent in parentJoin.DefaultIfEmpty()
                join rights in db.tblManage_MenuRights.Where(x => x.Usrno == Usrno)
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
                };


            return query.ToList();
        }
        public JsonResponse UserAccountActiveDeactive(string userId)
        {
            JsonResponse response = new JsonResponse();
            var objUser = db.tblMaster_User.Where(x => x.Mobile == userId || x.Email == userId || x.UserID == userId).FirstOrDefault();
            if (objUser != null)
            {
                objUser.IsActive = objUser.IsActive == true ? false : true;
                db.SaveChanges();
                response.status = 1;
                response.message = "Successfully Updated";
                return response;
            }
            else
            {
                response.status = 0;
                response.message = "Invalid User";
                return response;
            }
        }
        public JsonResponse CheckUserExist(string userId)
        {
            JsonResponse response = new JsonResponse();
            var objUser = db.tblMaster_User.Where(x => x.Mobile == userId || x.Email == userId || x.UserID == userId).FirstOrDefault();
            if (objUser != null)
            {
                response.status = 1;
                response.message = "Already Exist";
                return response;
            }
            var objAspNetUser = db.AspNetUsers.Where(x => x.PhoneNumber == userId || x.Email == userId).FirstOrDefault();
            if (objAspNetUser != null)
            {
                response.status = 1;
                response.message = "Already Exist";
                return response;
            }
            return response;
        }
        public AddEditUserModel UserDetail(int Usrno)
        {
            AddEditUserModel model = new AddEditUserModel();
            var objUser = db.View_Users.Where(x => x.Usrno == Usrno).FirstOrDefault();
            if (objUser != null)
            {
                model.Usrno = Usrno;
                model.UserTypeID = objUser.UserTypeID.ToString();
                model.UserType = objUser.UserType;
                model.AspNetID = objUser.AspNetID;
                model.UserID = objUser.UserID;
                model.Name = objUser.Name;
                model.Mobile = objUser.Mobile;
                model.Email = objUser.Email;
                model.CreatedByUserID = objUser.CreatedByUserID;
                model.IsKycCompleted = objUser.IsKycCompleted;
                model.RegisterOTP = objUser.RegisterOTP;
            }
            return model;
        }
        public JsonResponse AgencyKYC(AgentKycModel model)
        {
            JsonResponse response = new JsonResponse();
            var objAgentKYC = db.tblManage_AgentKYC.FirstOrDefault(e => e.Usrno == model.Usrno);
            if (objAgentKYC != null)
            {
                objAgentKYC.AgencyName = model.AgencyName;
                objAgentKYC.PanNumber = model.PanNumber;
                objAgentKYC.PancardImg = model.PancardImg;
                objAgentKYC.GstNumber = model.GSTNumber;
                objAgentKYC.BusinessTypeId = model.BusinessTypeID;
                objAgentKYC.AgencyAddress = model.AgencyAddress;
                objAgentKYC.Pincode = model.Pincode;
                objAgentKYC.City = model.City;
                objAgentKYC.State = model.State;
                objAgentKYC.KycDocumentTypeId = model.KYCDocumentTypeID;
                objAgentKYC.DocumentFile = model.DocumentFile;
                objAgentKYC.SupportingDocumentTypeId = model.SupportingDocumentTypeID;
                objAgentKYC.SupportingDocumentFile = model.SupportingDocumentFile;

                response.status = 1;
                response.message = "Update Successfull";
            }
            else
            {
                tblManage_AgentKYC dbAgentKYC = new tblManage_AgentKYC();
                dbAgentKYC.Usrno = SessionHelper.Usrno;
                dbAgentKYC.AgencyName = model.AgencyName;
                dbAgentKYC.PanNumber = model.PanNumber;
                dbAgentKYC.PancardImg = model.PancardImg;
                dbAgentKYC.GstNumber = model.GSTNumber;
                dbAgentKYC.BusinessTypeId = model.BusinessTypeID;
                dbAgentKYC.AgencyAddress = model.AgencyAddress;
                dbAgentKYC.Pincode = model.Pincode;
                dbAgentKYC.City = model.City;
                dbAgentKYC.State = model.State;
                dbAgentKYC.KycDocumentTypeId = model.KYCDocumentTypeID;
                dbAgentKYC.DocumentFile = model.DocumentFile;
                dbAgentKYC.SupportingDocumentTypeId = model.SupportingDocumentTypeID;
                dbAgentKYC.SupportingDocumentFile = model.SupportingDocumentFile;
                db.tblManage_AgentKYC.Add(dbAgentKYC);

                response.status = 1;
                response.message = "Submited Successfully";
            }
            db.SaveChanges();
            return response;
        }
        public AgentKycModel AgentKYCDetail(int Usrno)
        {
            AgentKycModel model = new AgentKycModel();
            var objAgentDocumentDetail = db.tblManage_AgentKYC.FirstOrDefault(e => e.Usrno == Usrno);
            if (objAgentDocumentDetail != null)
            {
                model.Usrno = objAgentDocumentDetail.Usrno;
                model.AgencyName = objAgentDocumentDetail.AgencyName;
                model.PanNumber = objAgentDocumentDetail.PanNumber;
                model.PancardImg = objAgentDocumentDetail.PancardImg;
                model.GSTNumber = objAgentDocumentDetail.GstNumber;
                model.AgencyTypeID = (int)objAgentDocumentDetail.AgencyTypeID;
                model.BusinessTypeID = (int)objAgentDocumentDetail.BusinessTypeId;
                model.KYCDocumentTypeID = (int)objAgentDocumentDetail.KycDocumentTypeId;
                model.SupportingDocumentTypeID = (int)objAgentDocumentDetail.SupportingDocumentTypeId;
                model.AgencyAddress = objAgentDocumentDetail.AgencyAddress;
                model.Pincode = objAgentDocumentDetail.Pincode;
                model.City = objAgentDocumentDetail.City;
                model.State = objAgentDocumentDetail.State;
                model.DocumentFile = objAgentDocumentDetail.DocumentFile;
                model.SupportingDocumentFile = objAgentDocumentDetail.SupportingDocumentFile;

            }
            return model;
        }
        public AgentDashboardSummary AgentDashboardSummary(int Usrno)
        {
            AgentDashboardSummary summary = new AgentDashboardSummary();
            var totalSales = 0;
            var totalCommission = 0;
            var totalBookings = 0;
            var totalSubUsers = db.tblMaster_User.Count(x => x.IntroUsrno == Usrno);
            summary.TotalSales = totalSales;
            summary.TotalCommission = totalCommission;
            summary.TotalBookings = totalBookings;
            summary.TotalSubUsers = totalSubUsers;
            return summary;
        }
        public List<SelectListItem> Country()
        {
            var list = new List<SelectListItem>();
            var objCountry = db.CountryMasters.ToList();
            list = objCountry.Select(e => new SelectListItem
            {
                Value = e.PhoneCode.Replace("+","").ToString(),
                Text = $"{e.CountryName} - ({e.PhoneCode})"
            }).ToList();

            return list;
        }
    }
}