using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelPortal.web.Models.Common;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Models.Interface
{
    public interface IUserServiceManager
    {
        List<SelectListItem> GetUserTypeList();
        JsonResponse AddEdit(UsersModels model);
        bool CheckEmailExist(string email);
        List<UserdetailViewModel> GetAll();
        UserdetailViewModel GetByUsrno(int usrno);
        UserdetailViewModel GetUserByEmail(string email);
        bool Delete(int id);
        bool ToggleActive(int id);
        JsonResponse UpdateProfile(UpdateProfileModel model);
        JsonResponse ChangePassword(ChangePasswordViewModel model);
        JsonResponse ForgotPassword(string email);
        JsonResponse ResetPassword(ResetPasswordViewModel model);
        JsonResponse UserInvited(List<UserInvitedModel> model,int ParentUsrno);
        List<InvitedUserViewModel> InvitedUsersList(SearchModel model);
    }
}