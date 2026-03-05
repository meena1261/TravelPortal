using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class AgentController : BaseController
    {
        private readonly IWalletServiceManager _walletservices;
        private readonly IUserServiceManager _userservice;
        private readonly IInventoryServiceManager _inventoryservices;
        private readonly Repository _repository;
        private readonly ApiClient _api = new ApiClient();
        private readonly db_silviEntities _db;
        public AgentController()
        {
            _walletservices = new WalletServiceManager();
            _userservice = new UserServiceManager();
            _inventoryservices = new InventoryServiceManager();
            _repository = new Repository();
            _db = new db_silviEntities();
        }
        // GET: Agent
        [Route(RouteConfig.AgentHome)]
        public ActionResult Home()
        {
            if (!SessionHelper.Islogin)
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.Profile = _userservice.GetByUsrno(SessionHelper.Usrno);
            return View();
        }
        [Route(RouteConfig.AgentDashboard)]
        [Route(RouteConfig.MyProfile)]
        [Route(RouteConfig.MyBooking)]
        [Route(RouteConfig.Wallet)]
        [Route(RouteConfig.FundRequest)]
        [Route(RouteConfig.CreditLimit)]
        [Route(RouteConfig.AgentSetting)]
        [Route(RouteConfig.UpdateProfile)]
        [Route(RouteConfig.Security)]
        [Route(RouteConfig.ManageInventory)]
        [Route(RouteConfig.ManageInventoryAdd)]
        [Route(RouteConfig.ManageInventoryList)]
        [Route(RouteConfig.ManageSubUsers)]
        public ActionResult AgentIndex()
        {
            if (!SessionHelper.Islogin)
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.Profile = _userservice.GetByUsrno(SessionHelper.Usrno);
            return View();
        }
        public ActionResult BussinessKYC()
        {
            var businessTypes = _db.tblMaster_BusinessType.ToList();
            ViewBag.BusinessTypeList = businessTypes.Select(x => new SelectListItem
            {
                Text = x.BusinessType,
                Value = x.ID.ToString()
            });

            var KYCDocumentType = _db.tblMaster_KYCDocumentType.ToList();
            ViewBag.KYCDocumentTypeList = KYCDocumentType.Select(x => new SelectListItem
            {
                Text = x.KYCDocumentType,
                Value = x.ID.ToString()
            });

            var SupportingDocumentType = _db.tblMaster_SupportingDocumentType.ToList();
            ViewBag.SupportingDocumentTypeList = SupportingDocumentType.Select(x => new SelectListItem
            {
                Text = x.SupportingDocumentType,
                Value = x.ID.ToString()
            });

            var AgencyType = _db.tblMaster_AgencyType.ToList();
            ViewBag.AgencyType = AgencyType.Select(x => new SelectListItem
            {
                Text = x.AgencyType,
                Value = x.ID.ToString()
            });

            AgentKycModel model = new AgentKycModel();
            model = _repository.AgentKYCDetail(SessionHelper.Usrno);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult BussinessKYC(AgentKycModel model)
        {
            JsonResponse response = new JsonResponse();
            model.Usrno = SessionHelper.Usrno;
            response = _repository.AgencyKYC(model);
            if (response.status > 0)
                SessionHelper.UserDetail = _userservice.GetByUsrno(SessionHelper.Usrno);
            return Json(response);
        }
        public ActionResult Dashboard()
        {
            ViewBag.Summary = _repository.AgentDashboardSummary(SessionHelper.Usrno);
            return PartialView();
        }
        public ActionResult MyProfile()
        {
            if (!SessionHelper.Islogin)
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.Profile = _userservice.GetByUsrno(SessionHelper.Usrno);

            return PartialView();
        }
        public ActionResult AccountSetting()
        {
            if (!SessionHelper.Islogin)
            {
                return RedirectToAction("Logout", "Account");
            }
            var objUser = _userservice.GetByUsrno(SessionHelper.Usrno);
            UpdateProfileModel model = new UpdateProfileModel();
            model.AgentID = objUser.UserId;
            model.Name = objUser.Name;
            model.Email = objUser.Email;
            model.Mobile = objUser.Mobile;
            model.Address = objUser.Address;
            model.City = objUser.City;
            model.State = objUser.State;
            model.PostalCode = objUser.PostalCode;
            model.Country = objUser.Country;
            model.ProfilePhoto = objUser.ProfilePhoto;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult UpdateProfile(UpdateProfileModel model)
        {
            JsonResponse response = new JsonResponse();
            model.Usrno = SessionHelper.Usrno;
            response = _userservice.UpdateProfile(model);
            SessionHelper.UserDetail = _userservice.GetByUsrno(SessionHelper.Usrno);
            return Json(response);
        }
        #region Wallet
        public ActionResult MyWallet()
        {
            if (!SessionHelper.Islogin)
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.walletSummary = _walletservices.WalletSummary(SessionHelper.Usrno);
            return PartialView();
        }
        public ActionResult WalletStatement(string Searchtext = "", string fromDate = "", string toDate = "")
        {
            SearchModel model = new SearchModel();
            model.SearchText = Searchtext;
            model.FromDate = fromDate;
            model.ToDate = toDate;
            model.Usrno = SessionHelper.Usrno;
            var objlist = _walletservices.WalletStatements(model);
            return PartialView(objlist);
        }
        public ActionResult WalletBalance()
        {
            string balance = _walletservices.GetWalletBalance(SessionHelper.Usrno);
            return PartialView("_WalletBalance", balance);
        }
        #endregion

        #region Payment Getway
        [HttpPost]
        public async Task<ActionResult> CreateOrder(decimal amount)
        {

            string orderId = await _walletservices.PaymentGetwayCreateOrder(SessionHelper.Usrno, amount);
            return Json(new { orderId = orderId }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> VerifyPayment(string paymentId, string orderId, string signature)
        {
            JsonResponse result = new JsonResponse();

            dynamic orderDetails = await _walletservices.PaymentGetwayVerifyPaymentAsync(SessionHelper.Usrno,paymentId, orderId, signature);

            if (orderDetails.status == 1)
            {
                //Credit user's wallet
                var creditResult = _walletservices.CreditDebit(new CreditDebitModel
                {
                    Usrno = SessionHelper.Usrno,
                    Amount = Convert.ToDecimal(orderDetails.data.amount),
                    Factor = "Cr", // Credit
                    Narration = "Wallet Recharge via Razorpay",
                    Remark = "Razorpay Payment ID: " + paymentId,
                    TransactionId = orderDetails.transactionId
                });
                if (creditResult.status == 1)
                {
                    result.status = 1;
                    result.message = "Payment successful and wallet credited.";
                }
                else
                {
                    result.status = 0;
                    result.message = "Payment successful but failed to credit wallet.";
                }
            }
            else
            {
                result.status = 0;
                result.message = "Payment verification failed.";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Fund Request
        public ActionResult FundRequest()
        {
            if (!SessionHelper.Islogin)
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.Paymentmode = DropdownLists.PaymentMode();
            ViewBag.fundSummary = _walletservices.FundRequestSummary(SessionHelper.Usrno);
            // ViewBag.list = AdoRepository.Report<dynamic>(type: "FundRequest", usrno: SessionHelper.Usrno);
            return PartialView();
        }
        public ActionResult FundRequestList(string Searchtext = "", string fromDate = "", string toDate = "", string status = "")
        {
            ViewBag.list = _walletservices.FundRequestList(new SearchModel
            {
                Usrno = SessionHelper.Usrno,
                SearchText = Searchtext,
                FromDate = fromDate,
                ToDate = toDate
            });
            return PartialView();
        }

        [HttpPost]
        public ActionResult RaiseFundRequest(FundRequestModel model)
        {
            JsonResponse result = new JsonResponse();
            if (ModelState.IsValid)
            {
                model.Usrno = SessionHelper.Usrno;
                result = _walletservices.FundRequest(model);
            }
            else
            {
                result.status = 0;
                result.message = "Please fill all required fields.";
            }
            return Json(result);
        }
        #endregion

        #region Credit limit
        public ActionResult CreditLimit()
        {
            ViewBag.CreditTypeList = _walletservices.CreditType();
            ViewBag.CreditSummary = _walletservices.CreditLimitSummary(SessionHelper.Usrno);
            return PartialView();
        }
        [HttpPost]
        public ActionResult RequestCreditLimit(CreditLimitRequestModel model)
        {
            JsonResponse response = new JsonResponse();
            if (SessionHelper.Islogin)
            {
                if (ModelState.IsValid)
                {
                    response = _walletservices.CreditLimitRequest(model);
                }
                else
                {
                    response.status = 0;
                    response.message = "Some field is missing";
                }
            }
            else
            {
                response.status = 0;
                response.message = "Session Expired";
            }
            return Json(response);
        }
        public ActionResult CreditLimitRequestList(string Searchtext = "", string fromDate = "", string toDate = "", string status = "")
        {
            ViewBag.list = _walletservices.CreditLimitRequestList(new SearchModel
            {
                Usrno = SessionHelper.Usrno,
                SearchText = Searchtext,
                FromDate = fromDate,
                ToDate = toDate
            });
            return PartialView();
        }
        #endregion

        #region Manage Inventory
        public ActionResult ManageInventory()
        {
            FlightInventoryServiceModel model = new FlightInventoryServiceModel();
            ViewBag.userdetail = _userservice.GetByUsrno(SessionHelper.Usrno);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult AddEditFlightInventory(FlightInventoryServiceModel model)
        {
            JsonResponse response = new JsonResponse();
            if (SessionHelper.UserDetail.IsSupplier != true)
            {
                response.status = 0;
                response.message = "You are not authorized to perform this action.";
                return Json(response);
            }
            if (ModelState.IsValid)
            {
                response = _inventoryservices.AddEditInventoryService(model);
            }
            else
            {
                response.status = 0;
                response.message = "Invalid";
            }
            return Json(response);
        }
        public ActionResult InventoryList(string Searchtext = "", string fromDate = "", string toDate = "", string status = "")
        {
            ViewBag.list = _inventoryservices.ListInventory(new SearchModel
            {
                Usrno = SessionHelper.Usrno,
                SearchText = Searchtext,
                FromDate = fromDate,
                ToDate = toDate
            });
            return PartialView();
        }

        [HttpPost]
        public ActionResult UploadSupllierAgreement(HttpPostedFileBase fileAgreement)
        {
            JsonResponse response = new JsonResponse();
            response = _inventoryservices.BecomeSupplier(new BecomeSupplierModel
            {
                Usrno = SessionHelper.Usrno,
                SupplierAgreementFile = fileAgreement
            });
            return Json(response);
        }
        #endregion

        #region Manage Sub Users
        public ActionResult ManageSubUsers()
        {
            ViewBag.userType = _userservice.GetUserTypeList().Where(e => e.Value == "2" || e.Value == "4").ToList();
            return PartialView();
        }
        public ActionResult AddEditSubUser(List<UserInvitedModel> model)
        {
            JsonResponse response = new JsonResponse();

            if (ModelState.IsValid)
            {
                response = _userservice.UserInvited(model, SessionHelper.Usrno);
            }
            else
            {
                response.status = 0;
                response.message = "Please fill all required fields.";
            }
            return Json(response);
        }
        public ActionResult SubUserList(string Searchtext = "", string fromDate = "", string toDate = "", string status = "")
        {
            ViewBag.list = _userservice.InvitedUsersList(new SearchModel
            {
                Usrno = SessionHelper.Usrno,
                SearchText = Searchtext,
                FromDate = fromDate,
                ToDate = toDate
            });
            return PartialView();
        }
        #endregion

        #region Reports
        public ActionResult BookingReports()
        {
            return PartialView();
        }
        #endregion
    }
}