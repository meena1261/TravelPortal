using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TravelPortal.EDMX;
using TravelPortal.web.Helpers;
using TravelPortal.web.Models;
using TravelPortal.web.Models.Common;
using TravelPortal.web.Models.Interface;
using TravelPortal.web.Models.Services;

namespace TravelPortal.web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ApplicationSignInManager _signInManager;
        public ApplicationUserManager _userManager;
        private readonly IAdminServiceManager _services;
        protected Repository repository = new Repository();
        public db_silviEntities db;
        public AdminController()
        {
            _services = new AdminServiceManager();
            db = new db_silviEntities();
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: Admin
        public ActionResult Dashboard()
        {
            if (!SessionHelper.IsAdminlogin)
            {
                return RedirectToAction("LogoutAdmin", "Account");
            }
            return View();
        }
        #region Manage Menu
        public ActionResult ManageMenu()
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            ViewBag.list = _services.MenuService.GetAll();
            return View();
        }
        public ActionResult AddEditMenu(int id = 0)
        {
            MenuModel model = new MenuModel();
            if (id > 0)
                model = _services.MenuService.GetById(id);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult AddEditMenu(MenuModel model)
        {
            JsonResponse response = _services.MenuService.AddEdit(model);
            return Json(response);
        }
        [HttpGet]
        public ActionResult DeleteMenu(int id)
        {
            var response = _services.MenuService.Delete(id);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetParentMenu(string term)
        {
            var list = _services.MenuService.GetAll();
            var filtered = list
                .Where(c => c.MenuName.StartsWith(term, StringComparison.OrdinalIgnoreCase))
                .Select(c => new
                {
                    label = $"{c.MenuName}",
                    value = $"{c.MenuName}",
                    id = c.ID
                }).ToList();

            return Json(filtered, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SideMenu()
        {
            ViewBag.list = _services.MenuService.GetUserMenu(SessionHelper.AdminUsrno);
            return PartialView("_SideMenuAdmin");
        }
        #endregion
        
        #region Coupons
        public ActionResult Coupons(int id = 0)
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("Login", "Account");
            ViewBag.Id = id;
            return View();
        }
        public ActionResult ListCoupons()
        {
            ViewBag.list = _services.CouponService.GetAll();
            return PartialView();
        }
        public ActionResult AddEditCoupons(int id = 0)
        {
            AddEditCouponsModel model = new AddEditCouponsModel();
            if (id > 0)
                model = _services.CouponService.GetById(id);

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult AddEditCoupons(AddEditCouponsModel model)
        {
            var response = _services.CouponService.AddEdit(model);
            return Json(response);
        }
        public ActionResult DeleteActiveCoupons(string type, string hdnId)
        {
            int id = Convert.ToInt32(hdnId);
            var response = _services.CouponService.Delete(id);
            ViewBag.list = _services.CouponService.GetAll();
            return PartialView("ListCoupons");
        }
        #endregion End Coupons

        #region Manage Admin
        public ActionResult ManageAdmin()
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ViewBag.list = AdoRepository.Report<dynamic>(type: "ListAdmin", usrno: SessionHelper.AdminUsrno);
            return View();
        }
        [HttpPost]
        public ActionResult ManageAdmin(SearchReportModel model)
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ViewBag.list = AdoRepository.Report<dynamic>(type: "ListAdmin", usrno: SessionHelper.AdminUsrno, fromDate: model.fromDate, toDate: model.toDate, DateRange: model.DateRange, userId: model.userId, status: model.status, IntroUsrno: model.IntroUsrno);
            return View();
        }
        public ActionResult AddEditAdmin(int id = 0)
        {
            AddEditUserModel model = new AddEditUserModel();
            if (id > 0)
                model = repository.UserDetail(id);

            model.CreatedByUserID = SessionHelper.AdminUsrno;
            // Convert roles into SelectListItem
            var userTypes = repository.ListUserTypes(SessionHelper.AdminUsrno).Where(e => e.ID != 1 && e.ID != 3 && e.ID != 4).Select(r => new SelectListItem
            {
                Text = r.UserType,        // what shows in dropdown
                Value = r.ID.ToString() // actual value (can be Id)
            }).ToList();
            ViewBag.userTypes = userTypes;
            return PartialView(model);
        }
        [HttpPost]
        public async Task<ActionResult> AddEditAdmin(AddEditUserModel model)
        {
            JsonResponse response = new JsonResponse();
            model.Password = ConfigHelper.DefaultPassword;
            model.ConfirmPassword = ConfigHelper.DefaultPassword;
            response = AdoRepository.AddEditUser(model);
            if (response.status > 0 && model.Usrno == 0)
            {
                var user = new ApplicationUser { PhoneNumber = model.Mobile, PhoneNumberConfirmed = true, Email = model.Email, UserName = model.Email };
                var result = await UserManager.CreateAsync(user, ConfigHelper.DefaultPassword);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin");
                    int usrno = Convert.ToInt32(response.status);
                    var objAgent = db.tblMaster_User.FirstOrDefault(e => e.Usrno == usrno);
                    if (objAgent != null)
                    {
                        objAgent.AspNetID = user.Id;
                        db.SaveChanges();
                    }
                    await SMS.ActivationAccount(model.Email, model.Name, response.data, response.status.ToString());
                }
            }
            return Json(response);
        }
        public async Task<ActionResult> SendActivationMail(int id)
        {
            var model = repository.UserDetail(id);
            await SMS.ActivationAccount(model.Email, model.Name, model.RegisterOTP, id.ToString());
            return Json(new JsonResponse { status = 1, message = "Activation mail sent successfully." }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ManageRoles()
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ViewBag.list = repository.ListUserTypes(SessionHelper.AdminUsrno).Where(e => e.ID != 1 && e.ID != 3 && e.ID != 4).ToList();
            return View();
        }
        public ActionResult AddEditRoles(int id = 0)
        {
            UserTypeModel model = new UserTypeModel();
            if (id > 0)
                model = repository.ListUserTypes(SessionHelper.AdminUsrno).Where(e => e.ID == id).FirstOrDefault();

            model.CreatedByUsrno = SessionHelper.AdminUsrno;
            return PartialView(model);
        }
        [HttpPost]
        public async Task<ActionResult> AddEditRoles(UserTypeModel model)
        {
            var response = repository.AddEditUserTypes(model);
            return Json(response);
        }
        public ActionResult AdminRights()
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var objAgent = db.View_Users.Where(e => e.RoleId == "1").ToList();
            ViewBag.adminlist = objAgent.Select(x => new SelectListItem
            {
                Text = x.Name + "-" + x.UserID,
                Value = x.Usrno.ToString()
            });
            return View();
        }
        public ActionResult ListAdminRights(int hdnUsrno)
        {
            var objParentMenu = repository.ListMenu(SessionHelper.AdminUsrno).Where(e => e.Ischeck > 0).ToList();
            var objUserMenu = repository.ListMenu(hdnUsrno);
            objParentMenu.ForEach(e => e.Ischeck = objUserMenu.FirstOrDefault(r => r.ID == e.ID).Ischeck);
            objParentMenu.ForEach(e => e.IsAdd = objUserMenu.FirstOrDefault(r => r.ID == e.ID).IsAdd);
            objParentMenu.ForEach(e => e.IsEdit = objUserMenu.FirstOrDefault(r => r.ID == e.ID).IsEdit);
            objParentMenu.ForEach(e => e.IsDelete = objUserMenu.FirstOrDefault(r => r.ID == e.ID).IsDelete);
            ViewBag.list = objParentMenu;
            return PartialView();
        }
        [HttpPost]
        public JsonResult SavePermissions(List<MenuRights> model)
        {
            JsonResponse response = new JsonResponse();
            if (model.Count > 0)
            {
                int Usrno = model[0].Usrno;
                var existingRights = db.tblManage_MenuRights.Where(x => x.Usrno == Usrno).ToList();
                db.tblManage_MenuRights.RemoveRange(existingRights);
                foreach (var item in model)
                {
                    tblManage_MenuRights menuRights = new tblManage_MenuRights();
                    menuRights.Usrno = item.Usrno;
                    menuRights.MenuID = item.ID;
                    menuRights.IsAdd = item.IsAdd == 1 ? true : false;
                    menuRights.IsEdit = item.IsEdit == 1 ? true : false;
                    menuRights.IsDelete = item.IsDelete == 1 ? true : false;
                    db.tblManage_MenuRights.Add(menuRights);
                    db.SaveChanges();
                }
                response.status = 1;
                response.message = "Menu Rights Updated Successfully";
            }
            else
            {
                response.status = 0;
                response.message = "No Menu Rights to Update";
            }
            return Json(response);
        }

        public JsonResult GetAdminList(string term)
        {
            var list = db.View_Users.Where(e => e.RoleId == "1").ToList();
            var filtered = list.Where(c => c.Name.StartsWith(term, StringComparison.OrdinalIgnoreCase))
         .Select(c => new
         {
             label = $"{c.Name}-[{c.UserID}]",
             value = $"{c.Name}-[{c.UserID}]",
             id = c.Usrno
         })
         .ToList();

            return Json(filtered, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Manage Agent
        public ActionResult ManageAgent()
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ViewBag.list = AdoRepository.Report<dynamic>(type: "ListAgents", usrno: SessionHelper.AdminUsrno);
            return View();
        }
        [HttpPost]
        public ActionResult ManageAgent(SearchReportModel model)
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ViewBag.list = AdoRepository.Report<dynamic>(type: "ListAgents", usrno: SessionHelper.AdminUsrno, fromDate: model.fromDate, toDate: model.toDate, DateRange: model.DateRange, userId: model.userId, status: model.status, IntroUsrno: model.IntroUsrno);
            return View();
        }
        [HttpPost]
        public ActionResult UpdateAgentPermission(string type, int Id)
        {
            var objUser = db.tblMaster_User.FirstOrDefault(e => e.Usrno == Id);
            if (type.Equals(ActionTypes.IsActive))
                objUser.IsActive = objUser.IsActive == false ? true : false;
            else if (type.Equals(ActionTypes.IsSupplier))
                objUser.IsSupplier = objUser.IsSupplier == false ? true : false;
            else if (type.Equals(ActionTypes.IsSales))
                objUser.IsSales = objUser.IsSales == false ? true : false;
            else if (type.Equals(ActionTypes.IsPaymentByCC))
                objUser.IsPaymentByCC = objUser.IsPaymentByCC == false ? true : false;
            else if (type.Equals(ActionTypes.IsPaymentByAccount))
                objUser.IsPaymentByAccount = objUser.IsPaymentByAccount == false ? true : false;
            else if (type.Equals(ActionTypes.IsUseLogo))
                objUser.IsUseLogo = objUser.IsUseLogo == false ? true : false;
            else if (type.Equals(ActionTypes.IsContract))
                objUser.IsContract = objUser.IsContract == false ? true : false;
            else if (type.Equals(ActionTypes.IsToken))
                objUser.IsToken = objUser.IsToken == false ? true : false;

            db.SaveChanges();
            return Json("Success");
        }
        public ActionResult AddEditAgent(int id = 0)
        {
            AddEditUserModel model = new AddEditUserModel();
            if (id > 0)
                model = repository.UserDetail(id);

            model.CreatedByUserID = SessionHelper.AdminUsrno;
            // Convert roles into SelectListItem
            var userTypes = repository.ListUserTypes(SessionHelper.AdminUsrno).Where(e => e.ID == 3).Select(r => new SelectListItem
            {
                Text = r.UserType,        // what shows in dropdown
                Value = r.ID.ToString() // actual value (can be Id)
            }).ToList();
            ViewBag.userTypes = userTypes;
            return PartialView(model);
        }
        [HttpPost]
        public async Task<ActionResult> AddEditAgent(AddEditUserModel model)
        {
            JsonResponse response = new JsonResponse();
            model.Password = ConfigHelper.DefaultPassword;
            model.ConfirmPassword = ConfigHelper.DefaultPassword;
            response = AdoRepository.AddEditUser(model);
            if (response.status > 0 && model.Usrno == 0)
            {
                var user = new ApplicationUser { PhoneNumber = model.Mobile, PhoneNumberConfirmed = true, Email = model.Email, UserName = model.Email };
                var result = await UserManager.CreateAsync(user, ConfigHelper.DefaultPassword);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Agent");
                    int usrno = Convert.ToInt32(response.status);
                    var objAgent = db.tblMaster_User.FirstOrDefault(e => e.Usrno == usrno);
                    if (objAgent != null)
                    {
                        objAgent.AspNetID = user.Id;
                        db.SaveChanges();
                    }
                    await SMS.ActivationAccount(model.Email, model.Name, response.data, response.status.ToString());
                }
            }
            return Json(response);
        }
        public ActionResult AgentUsers()
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var objAgent = db.View_Users.Where(e => e.RoleId == "2").ToList();
            ViewBag.agentlist = objAgent.Select(x => new SelectListItem
            {
                Text = x.Name + "-" + x.UserID,
                Value = x.Usrno.ToString()
            });
            return View();
        }
        [HttpPost]
        public ActionResult ListAgentUsers(int hdnUsrno)
        {
            ViewBag.list = AdoRepository.Report<dynamic>(type: "ListAgents", usrno: SessionHelper.AdminUsrno, IntroUsrno: hdnUsrno);
            return PartialView();
        }
        public ActionResult AgentKycRequest()
        {
            if (!SessionHelper.IsAdminlogin)
            {
                return RedirectToAction("LogoutAdmin", "Account");
            }
            ViewBag.Paymentmode = DropdownLists.PaymentMode();
            ViewBag.list = AdoRepository.Report<dynamic>(type: "AgentKycRequest", usrno: SessionHelper.AdminUsrno);
            return View();
        }
        [HttpPost]
        public ActionResult AgentKycRequest(SearchReportModel model)
        {
            if (!SessionHelper.IsAdminlogin)
            {
                return RedirectToAction("LogoutAdmin", "Account");
            }
            ViewBag.Paymentmode = DropdownLists.PaymentMode();

            ViewBag.list = AdoRepository.Report<dynamic>(type: "AgentKycRequest", usrno: SessionHelper.AdminUsrno, fromDate: model.fromDate, toDate: model.toDate, DateRange: model.DateRange, userId: model.userId, status: model.status, IntroUsrno: model.IntroUsrno);
            return View(model);
        }
        [HttpPost]
        public ActionResult AgentKycRequestAction(ApproveRejectActionModel model)
        {
            //var response = repository.AgnetKYCApproveReject(model);
            return Json("");
        }


        public ActionResult FundRequest()
        {
            if (!SessionHelper.IsAdminlogin)
            {
                return RedirectToAction("LogoutAdmin", "Account");
            }
            ViewBag.Paymentmode = DropdownLists.PaymentMode();

            ViewBag.list = AdoRepository.Report<dynamic>(type: "FundRequest", usrno: SessionHelper.AdminUsrno);
            return View();
        }
        [HttpPost]
        public ActionResult FundRequest(SearchReportModel model)
        {
            if (!SessionHelper.IsAdminlogin)
            {
                return RedirectToAction("LogoutAdmin", "Account");
            }
            ViewBag.Paymentmode = DropdownLists.PaymentMode();
            ViewBag.list = AdoRepository.Report<dynamic>(type: "FundRequest", usrno: SessionHelper.AdminUsrno, fromDate: model.fromDate, toDate: model.toDate, DateRange: model.DateRange, userId: model.userId, status: model.status, IntroUsrno: model.IntroUsrno);
            return View(model);
        }
        [HttpPost]
        public ActionResult FundRequestAction(FundRequestActionModel model)
        {
            JsonResponse response = new JsonResponse();
            var objFundRequest = db.tblHistory_FundRequest.FirstOrDefault(e => e.ID == model.Id && e.IsApproved == false && e.IsRejected == false);
            if (objFundRequest != null)
            {
                objFundRequest.IsApproved = model.Action.ToLower() == "Approved".ToLower() ? true : (bool?)null;
                objFundRequest.IsRejected = model.Action.ToLower() == "Rejected".ToLower() ? true : (bool?)null;
                objFundRequest.ActionDate = DateTime.Now;
                objFundRequest.Remark = model.Reason;
                if (model.Action.ToLower() == "Approved".ToLower())
                {
                    string narration = $"Fund Request Approved of Amount {objFundRequest.Amount} for UTR No {objFundRequest.UTRNo}";
                    AdoRepository.MainWalletCreditDebit(Usrno: (int)objFundRequest.Usrno, Amount: (decimal)objFundRequest.Amount, Factor: "Cr", Narration: narration);
                }
                db.SaveChanges();
                response.status = 1;
                response.message = $"{model.Action} Successfully";
            }
            else
            {
                response.status = 0;
                response.message = "Fund Request not found.";
            }
            return Json(response);
        }
        public ActionResult AgentWallet()
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ViewBag.list = AdoRepository.Report<dynamic>(type: "AgentWallet", usrno: SessionHelper.AdminUsrno);
            return View();
        }
        [HttpPost]
        public ActionResult AgentWallet(SearchReportModel model)
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ViewBag.list = AdoRepository.Report<dynamic>(type: "AgentWallet", usrno: SessionHelper.AdminUsrno, fromDate: model.fromDate, toDate: model.toDate, DateRange: model.DateRange, userId: model.userId, status: model.status, IntroUsrno: model.IntroUsrno);
            return View();
        }
        public ActionResult WalletStatement(int id)
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ViewBag.walletStatement = AdoRepository.Report<dynamic>(type: "WalletStatement", usrno: id);
            return View();
        }
        [HttpPost]
        public ActionResult WalletStatement(SearchReportModel model)
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ViewBag.walletStatement = AdoRepository.Report<dynamic>(type: "WalletStatement", usrno: SessionHelper.AdminUsrno, fromDate: model.fromDate, toDate: model.toDate, DateRange: model.DateRange, userId: model.userId, status: model.status, IntroUsrno: model.IntroUsrno);
            return View();
        }
        public ActionResult CreditDebitFund(int usrno, string factor = "")
        {
            ViewBag.factor = DropdownLists.Factor();
            DebitCreditFundModel model = new DebitCreditFundModel();
            model.Usrno = usrno;
            model.Factor = factor;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult CreditDebitFund(DebitCreditFundModel model)
        {
            var response = AdoRepository.MainWalletCreditDebit(model.Usrno, Convert.ToDecimal(model.Amount), model.Factor, model.Remark);
            return Json(response);
        }
        #endregion

        #region Credit Limit Management
        public ActionResult CreditLimitDashboard()
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ViewBag.CreditLimitAgents = AdoRepository.Report<dynamic>(type: "CreditLimitAgents");
            ViewBag.creditrequestlist = AdoRepository.Report<dynamic>(type: "AgentCreditLimitRequest");

            CreditLimitApproveRejectModel model = new CreditLimitApproveRejectModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreditLimitApproveReject(CreditLimitApproveRejectModel model)
        {
            JsonResponse response = new JsonResponse();
            var objCreditLimit = db.tblManage_CreditLimit.FirstOrDefault(e => e.ID == model.Id);
            if (objCreditLimit != null)
            {
                objCreditLimit.IsApproved = model.Action.ToLower() == "Approved".ToLower() ? true : (bool?)null;
                objCreditLimit.IsRejected = model.Action.ToLower() == "Rejected".ToLower() ? true : (bool?)null;
                objCreditLimit.Days = string.IsNullOrEmpty(model.Days) ? 0 : Convert.ToInt32(model.Days);
                objCreditLimit.ActionByUsrno = SessionHelper.AdminUsrno;
                objCreditLimit.ActionDate = DateTime.Now;
                objCreditLimit.AdminRemark = model.Reason;
                if (model.Action.ToLower() == "Approved".ToLower())
                {
                    string narration = $"Request Approved of Amount {objCreditLimit.Amount} for {model.Reason}";
                    AdoRepository.CreditWalletCreditDebit(Usrno: (int)objCreditLimit.Usrno, Amount: (decimal)objCreditLimit.Amount, Factor: "Cr", Narration: narration);
                }
                db.SaveChanges();
                response.status = 1;
                response.message = "Approved Successfully";
            }

            return Json(response);
        }
        #endregion

        #region Markup Setting
        public ActionResult MarkupSetting()
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            ViewBag.MarkupTypes = DropdownLists.MarkupType();
            ViewBag.MarkupMethodType = DropdownLists.MarkupMethodType();
            ViewBag.MarkupCategories = DropdownLists.MarkupCategories();
            //var objCurrency = db.Currencies.Select(r => new SelectListItem
            //{
            //    Text = r.CurrencyCode,        // what shows in dropdown
            //    Value = r.CurrencyId.ToString() // actual value (can be Id)
            //}).ToList();
            //ViewBag.CurrencyList = objCurrency;
            var objAgent = db.View_Users.Where(e => e.RoleId == "2").ToList();
            ViewBag.agentlist = objAgent.Select(x => new SelectListItem
            {
                Text = x.Name + "-" + x.UserID,
                Value = x.Usrno.ToString()
            });
            return View();
        }
        [HttpPost]
        public ActionResult MarkupSetting(MarkupSettingModel model)
        {
            JsonResponse response = new JsonResponse();
            var dbMarkup = db.tblManage_Markup.FirstOrDefault(e => e.ID == model.Id);
            if (dbMarkup == null)
            {
                tblManage_Markup newMarkup = new tblManage_Markup();
                string[] dates = model.DateRange.Split('-');
                model.StartDate = dates[0];
                model.EndDate = dates[1];
                newMarkup.MarkupTypeID = model.MarkupTypeID == "" ? (int?)null : Convert.ToInt32(model.MarkupTypeID);
                newMarkup.Value = model.MarkupValue;
                newMarkup.CurrencyID = model.CurrencyID == "" ? (int?)null : Convert.ToInt32(model.CurrencyID);
                newMarkup.MethodID = model.MarkupMethodType == "" ? (int?)null : Convert.ToInt32(model.MarkupMethodType);
                newMarkup.StartDate = string.IsNullOrEmpty(model.StartDate) ? (DateTime?)null : Convert.ToDateTime(model.StartDate);
                newMarkup.EndDate = string.IsNullOrEmpty(model.EndDate) ? (DateTime?)null : Convert.ToDateTime(model.EndDate);
                newMarkup.MarkupCategoryId = model.MarkupCategoryId == "" ? (int?)null : Convert.ToInt32(model.MarkupCategoryId);
                newMarkup.Usrno = model.Usrno == "" ? (int?)null : Convert.ToInt32(model.Usrno);
                newMarkup.AirlineCode = model.AirlineId;
                newMarkup.CreatedByUsrno = SessionHelper.AdminUsrno;
                newMarkup.IsUniversal = false;
                newMarkup.IsUniversalOverride = false;
                newMarkup.IsActive = true;
                newMarkup.AddDate = DateTime.Now;
                db.tblManage_Markup.Add(newMarkup);
                db.SaveChanges();
                response.status = 1;
                response.message = "Markup updated successfully.";
            }
            else
            {
                response.status = 0;
                response.message = "Markup not found.";
            }
            return Json(response);
        }
        #endregion

        #region Comman 
        public ActionResult UserDetail(string userid)
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            int usrno = Convert.ToInt32(userid);
            return View(usrno);
        }
        public ActionResult ResetPassword()
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var objAgent = db.View_Users.Where(e => e.RoleId == "2").ToList();
            ViewBag.agentlist = objAgent.Select(x => new SelectListItem
            {
                Text = x.Name + "-" + x.UserID,
                Value = x.Usrno.ToString()
            });
            PasswordResetByAdminModel model = new PasswordResetByAdminModel();
            model.Password = ConfigHelper.DefaultPassword;
            return View(model);
        }
        public async Task<ActionResult> ResetUserPassword(PasswordResetByAdminModel model)
        {
            JsonResponse response = new JsonResponse();
            int hdnUsrno = Convert.ToInt32(model.Id);
            var objUser = db.tblMaster_User.FirstOrDefault(e => e.Usrno == hdnUsrno);
            if (objUser != null)
            {
                var user = UserManager.FindById(objUser.AspNetID);
                if (user != null)
                {
                    string resetToken = UserManager.GeneratePasswordResetToken(user.Id);
                    var result = await UserManager.ResetPasswordAsync(user.Id, resetToken, model.Password);
                    if (result.Succeeded)
                    {
                        objUser.Password = model.Password;
                        db.SaveChanges();
                        //await SMS.ResetPasswordNotification(objUser.Email, objUser.Name, ConfigHelper.DefaultPassword);
                        response.status = 1;
                        response.message = "Password reset successfully.";
                    }
                    else
                    {
                        response.status = 0;
                        response.message = "Error resetting password.";
                    }
                }
                else
                {
                    response.status = 0;
                    response.message = "User not found in ASP.NET Identity.";
                }
            }
            else
            {
                response.status = 0;
                response.message = "User not found.";
            }
            return Json(response);
        }
        #endregion

        #region API Management
        public ActionResult ApiManagement()
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            return View();
        }
        public ActionResult AddEditAPISupplier(int id = 0)
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            AddEditAPISupplierModel model = new AddEditAPISupplierModel();
            if (id > 0)
            {
                var objAPI = db.tblManage_API.Find(id);
                if (objAPI != null)
                {
                    model.APIID = objAPI.APIID;
                    model.Supplier = objAPI.Supplier;
                    model.APIType = objAPI.APIType;
                    model.LiveEndPointUrl = objAPI.LiveEndPointUrl;
                    model.LiveClientId = objAPI.LiveClientId;
                    model.LiveClientSecret = objAPI.LiveClientSecret;
                    model.TestEndPointUrl = objAPI.TestEndPointUrl;
                    model.TestClientId = objAPI.TestClientId;
                    model.TestClientSecret = objAPI.TestClientSecret;
                }
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult AddEditAPISupplier(AddEditAPISupplierModel model)
        {
            JsonResponse response = new JsonResponse();
            if (ModelState.IsValid == false)
            {
                response.status = 0;
                response.message = "Please fill all required fields.";
                return Json(response);
            }
            if (model.APIID > 0)
            {
                var objAPI = db.tblManage_API.Find(model.APIID);
                if (objAPI != null)
                {
                    objAPI.APIID = model.APIID;
                    objAPI.Supplier = model.Supplier;
                    objAPI.APIType = model.APIType;
                    objAPI.LiveEndPointUrl = model.LiveEndPointUrl;
                    objAPI.LiveClientId = model.LiveClientId;
                    objAPI.LiveClientSecret = model.LiveClientSecret;
                    objAPI.TestEndPointUrl = model.TestEndPointUrl;
                    objAPI.TestClientId = model.TestClientId;
                    objAPI.TestClientSecret = model.TestClientSecret;
                    db.SaveChanges();
                    response.status = 1;
                    response.message = "API Supplier updated successfully.";
                }
            }
            else
            {
                tblManage_API newApi = new tblManage_API();
                newApi.APIID = model.APIID;
                newApi.Supplier = model.Supplier;
                newApi.APIType = model.APIType;
                newApi.LiveEndPointUrl = model.LiveEndPointUrl;
                newApi.LiveClientId = model.LiveClientId;
                newApi.LiveClientSecret = model.LiveClientSecret;
                newApi.TestEndPointUrl = model.TestEndPointUrl;
                newApi.TestClientId = model.TestClientId;
                newApi.TestClientSecret = model.TestClientSecret;
                newApi.AddDate = DateTime.Now;
                newApi.IsActive = true;
                db.tblManage_API.Add(newApi);
                db.SaveChanges();
                var objAgent = db.View_Users.Where(e => e.RoleId == "2").ToList();
                if (objAgent.Count > 0)
                {
                    foreach (var agent in objAgent)
                    {
                        tblManage_AssignAPI newAgentAPI = new tblManage_AssignAPI();
                        newAgentAPI.Usrno = (int)agent.Usrno;
                        newAgentAPI.APIID = newApi.APIID;
                        newAgentAPI.IsActive = false;
                        db.tblManage_AssignAPI.Add(newAgentAPI);
                        db.SaveChanges();
                    }
                }
                response.status = 1;
                response.message = "API Supplier added successfully.";
            }
            return Json(response);
        }
        public ActionResult APIActiveDeactive(string type, int apiId, int usrno = 0)
        {
            if (type.ToLower() == "IsActive".ToLower() && apiId > 0)
            {
                if (usrno > 0)
                {
                    var api = db.tblManage_AssignAPI.FirstOrDefault(e => e.APIID == apiId && e.Usrno == usrno);
                    if (api != null)
                    {
                        api.IsActive = api.IsActive == true ? false : true;
                        db.SaveChanges();
                    }
                }
                else
                {
                    var api = db.tblManage_API.FirstOrDefault(e => e.APIID == apiId);
                    if (api != null)
                    {
                        api.IsActive = api.IsActive == true ? false : true;
                        db.SaveChanges();
                    }
                }
            }
            else if (type.ToLower() == "IsLive".ToLower() && apiId > 0)
            {
                var api = db.tblManage_API.FirstOrDefault(e => e.APIID == apiId);
                if (api != null)
                {
                    api.IsLive = api.IsLive == true ? false : true;
                    db.SaveChanges();
                }
            }
            return null;
        }
        #endregion

        #region Look To Book Ratio management
        public ActionResult LookToBookRatio()
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            return View();
        }
        public ActionResult UpdateAgentFlightLookLimit(int hdnUsrno, string PeriodType, decimal LookLimit)
        {
            JsonResponse response = new JsonResponse();
            var objAgent = db.tblMaster_User.FirstOrDefault(e => e.Usrno == hdnUsrno);
            if (objAgent != null)
            {
                objAgent.LookLimitPeriodType = PeriodType;
                objAgent.FlightLookLimit = (int)LookLimit;
                db.SaveChanges();
                response.status = 1;
                response.message = "Flight Look Limit updated successfully.";
            }
            else
            {
                response.status = 0;
                response.message = "Agent not found.";
            }
            return Json(response);
        }
        #endregion

        #region
        public ActionResult SupplierRequest()
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            return View();
        }
        [HttpPost]
        public ActionResult SupplierRequest(SearchReportModel model)
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            return View(model);
        }
        [HttpPost]
        public ActionResult SupplierRequestApproveReject(ApproveRejectActionModel model)
        {
            JsonResponse response = new JsonResponse();
            var objUser = db.tblMaster_User.FirstOrDefault(e => e.Usrno == model.Id);
            if (model.Action.Equals("Approved"))
            {
                objUser.IsSupplier = true;
                response.status = 1;
                response.message = "Approved Successfully";
            }
            else if (model.Action.Equals("Rejected"))
            {
                objUser.IsSupplier = null;
                objUser.AgreementRemark = model.Reason;
                response.status = 1;
                response.message = "Rejected Successfully";
            }
            else
            {
                response.status = 0;
                response.message = "Invalid Action";
            }
            db.SaveChanges();
            return Json(response);
        }
        public ActionResult ManageInventory()
        {
            if (!SessionHelper.IsAdminlogin)
                return RedirectToAction("LogoutAdmin", "Account");
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            return View();
        }
        [HttpPost]
        public ActionResult InventoryRequestApproveReject(ApproveRejectActionModel model)
        {
            JsonResponse response = new JsonResponse();
            var objInventory = db.tblFlightInventories.FirstOrDefault(e => e.FlightInventoryID == model.Id);
            if (objInventory != null)
            {

                if (model.Action.Equals("Approved"))
                {
                    objInventory.IsApproved = true;
                    response.status = 1;
                    response.message = "Approved Successfully";
                }
                else if (model.Action.Equals("Rejected"))
                {
                    objInventory.IsRejected = null;
                    objInventory.Remark = model.Reason;
                    response.status = 1;
                    response.message = "Rejected Successfully";
                }
            }
            else
            {
                response.status = 0;
                response.message = "Invalid Action";
            }
            db.SaveChanges();
            return Json(response);
        }
        #endregion
    }
}