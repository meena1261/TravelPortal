using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using TravelPortal.EDMX;
using TravelPortal.web.Helpers;
using TravelPortal.web.Models.Common;
using TravelPortal.web.Models.Interface;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Models.Services
{
    public class UserServiceManager : IUserServiceManager
    {
        private readonly db_silviEntities _context;
        public UserServiceManager()
        {
            _context = new db_silviEntities();
        }
        public List<SelectListItem> GetUserTypeList()
        {
            var list = new List<SelectListItem>();
            var userTypes = _context.tblMaster_UserType.Where(ut => ut.IsActive == true).ToList();
            foreach (var type in userTypes)
            {
                list.Add(new SelectListItem
                {
                    Text = type.UserType,
                    Value = type.ID.ToString()
                });
            }
            return list;
        }
        public JsonResponse AddEdit(UsersModels model)
        {
            var response = new JsonResponse();
            try
            {
                var objUser = _context.tblMaster_User.Where(u => u.Usrno == model.Usrno).FirstOrDefault();
                if (objUser == null)
                {
                    var result = _context.ProcMaster_User(
                        "AddEditUser",
                        model.Usrno,
                        model.Mobile,
                        model.Name,
                        model.Email,
                        model.AspNetID,
                        "",
                        !string.IsNullOrEmpty(model.Password) ? model.Password : ConfigHelper.DefaultPassword,
                        model.ReferralCode,
                        model.CreatedbyUsrno,
                        model.UserTypeID
                        ).FirstOrDefault();

                    response.status = result.Status;
                    response.message = result.Message;
                }
                else
                {
                    objUser.Name = model.Name;
                    objUser.Address = model.Address;
                    objUser.City = model.City;
                    objUser.State = model.State;
                    objUser.Pincode = model.PostalCode;
                    _context.SaveChanges();
                    response.status = 1;
                    response.message = "User updated successfully.";
                }
            }
            catch (Exception ex)
            {
                response.status = 0;
                response.message = "An error occurred: " + ex.Message;
                response.data = null;
            }
            return response;
        }
        public bool CheckEmailExist(string email)
        {
            var user = _context.AspNetUsers.Where(u => u.Email == email).FirstOrDefault();
            return user != null;
        }
        public List<UserdetailViewModel> GetAll()
        {
            var list = new List<UserdetailViewModel>();
            var users = _context.tblMaster_User.ToList();
            foreach (var user in users)
            {
                list.Add(new UserdetailViewModel
                {
                    Usrno = (int)user.Usrno,
                    UserId = user.UserID,
                    Name = user.Name,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Address = user.Address,
                    City = user.City,
                    State = user.State,
                    PostalCode = user.Pincode,
                    IsKYCCompleted = user.IsKycCompleted,
                    IsSupplier = user.IsSupplier,
                    UserTypeID = (int)user.UserTypeID,
                    CreatedDate = user.AddDate.ToString("dd-MM-yyyy"),
                });
            }
            return list;
        }
        public UserdetailViewModel GetByUsrno(int usrno)
        {
            var user = _context.tblMaster_User.Find(usrno);
            if (user != null)
            {
                return new UserdetailViewModel
                {
                    Usrno = (int)user.Usrno,
                    UserId = user.UserID,
                    AspNetID = user.AspNetID,
                    Name = user.Name,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Address = user.Address,
                    City = user.City,
                    State = user.State,
                    Country = user.Country,
                    PostalCode = user.Pincode,
                    ProfilePhoto = user.Photo,
                    IsKYCCompleted = user.IsKycCompleted,
                    IsSupplier = user.IsSupplier,
                    SupplierAgreement = user.SupplierAgreement,
                    AgreementRemark = user.AgreementRemark,
                    UserTypeID = (int)user.UserTypeID,
                    CreatedDate = user.AddDate.ToLongDateString(),
                };
            }
            return null;
        }
        public UserdetailViewModel GetUserByEmail(string email)
        {
            var user = _context.View_Users.Where(u => u.Email == email).FirstOrDefault();
            if (user != null)
            {
                return new UserdetailViewModel
                {
                    Usrno = (int)user.Usrno,
                    UserId = user.UserID,
                    Name = user.Name,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    IsKYCCompleted = user.IsKycCompleted ?? false,
                    IsSupplier = user.IsSupplier ?? false,
                    UserTypeID = Convert.ToInt32(user.UserTypeID),
                    RoleId = Convert.ToInt32(user.RoleId),
                    CreatedDate = user.AddDate.ToString("dd-MM-yyyy"),
                };
            }
            return null;
        }
        public bool Delete(int id)
        {
            var user = _context.tblMaster_User.Find(id);
            if (user != null)
            {
                _context.tblMaster_User.Remove(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool ToggleActive(int id)
        {
            var user = _context.tblMaster_User.Find(id);
            if (user != null)
            {
                user.IsActive = !user.IsActive;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public JsonResponse UpdateProfile(UpdateProfileModel model)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                var objUser = _context.tblMaster_User.FirstOrDefault(e=>e.Usrno == model.Usrno);
                if (objUser != null)
                {
                    if (model.UploadProfilePhoto != null && model.UploadProfilePhoto.ContentLength > 0)
                    {
                        response = FileUploadHelper.FileUpload(model.UploadProfilePhoto, "Profile", objUser.UserID);
                        if (response.status == 0)
                            return response;
                        model.ProfilePhoto = response.message;
                    }
                    objUser.Name = model.Name;
                    //objUser.Email = model.Email;
                    objUser.Mobile = model.Mobile;
                    objUser.Address = model.Address;
                    objUser.City = model.City;
                    objUser.State = model.State;
                    objUser.Country = model.Country;
                    objUser.Pincode = model.PostalCode;
                    objUser.Photo = model.ProfilePhoto;
                    _context.SaveChanges();
                    response.status = 1;
                    response.message = "Update Successfully";
                    return response;
                }
                response.status = 0;
                response.message = "Invalid User";
                return response;
            }
            catch (Exception ex)
            {
                response.status = 0;
                response.message = "Same thing went wrong";
            }
            return response;
        }
        public JsonResponse ChangePassword(ChangePasswordViewModel model)
        {
            throw new NotImplementedException();
        }
        public JsonResponse ForgotPassword(string email)
        {
            throw new NotImplementedException();
        }
        public JsonResponse ResetPassword(ResetPasswordViewModel model)
        {
            throw new NotImplementedException();
        }
        public JsonResponse UserInvited(List<UserInvitedModel> model, int ParentUsrno)
        {
            JsonResponse response = new JsonResponse();
            try
            {

                if (model.Count > 0)
                {
                    foreach (var item in model)
                    {
                        tblManage_UserInvited dbUserInvited = new tblManage_UserInvited();
                        dbUserInvited.RoleId = Convert.ToInt32(item.RoleId);
                        dbUserInvited.Name = item.Name;
                        dbUserInvited.Email = item.Email;
                        dbUserInvited.Mobile = item.Mobile;
                        dbUserInvited.InvitedUsrno = ParentUsrno;
                        dbUserInvited.IsActive = true;
                        dbUserInvited.AddDate = DateTime.Now;
                        _context.tblManage_UserInvited.Add(dbUserInvited);
                        _context.SaveChanges();
                    }
                }
                response.status = 1;
                response.message = "Successfully Send Invitation";
            }
            catch (Exception ex)
            {
                response.status = 0;
                response.message = "An error occurred: " + ex.Message;
            }
            return response;
        }
        public List<InvitedUserViewModel> InvitedUsersList(SearchModel model)
        {
            var list = new List<InvitedUserViewModel>();
            var users = _context.tblManage_UserInvited.Where(u => u.InvitedUsrno == model.Usrno || (model.Usrno) == 0).ToList();
            foreach (var user in users)
            {
                list.Add(new InvitedUserViewModel
                {
                    InvitedUserId = user.ID,
                    UserName = user.Name,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Status = user.IsActive == true ? "Active" : "Inactive",
                    InvitedDate = user.AddDate.HasValue ? user.AddDate.Value.ToString("dd-MM-yyyy") : "",
                    UserType = _context.tblMaster_UserType.Where(r => r.ID == user.RoleId).Select(r => r.UserType).FirstOrDefault()
                });
            }
            return list;
        }
    }
}