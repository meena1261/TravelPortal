using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TravelPortal.EDMX;
using TravelPortal.web.Helpers;
using TravelPortal.web.Models.Common;
using TravelPortal.web.Models.Interface;
using TravelPortal.web.Models.ViewModel;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace TravelPortal.web.Models.Services
{
    public class WalletServiceManager : IWalletServiceManager
    {
        private readonly ApiClient _api = new ApiClient();
        private readonly db_silviEntities _context;
        public WalletServiceManager()
        {
            _context = new db_silviEntities();
        }
        public string GetWalletBalance(int Usrno)
        {
            var balance = "0.00";
            if (Usrno > 0)
            {
                balance = _context.tblAccount_Main.FirstOrDefault(e => e.UsrNo == Usrno).Balance.ToString();
            }
            return balance;
        }
        public JsonResponse CreditDebit(CreditDebitModel model)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                var obj = _context.ProcMaster_MainWallet(
                    model.Usrno,
                    model.Amount,
                    model.Factor,
                    model.Narration,
                    1,
                    model.Remark,
                    model.TransactionId
                    ).FirstOrDefault();
                if (obj != null)
                {
                    response.status = obj.Status;
                    response.message = obj.Message;
                }
            }
            catch (Exception ex)
            {
                response.status = 0;
                response.message = ex.Message;
            }
            return response;
        }
        public WalletSummaryViewModel WalletSummary(int Usrno)
        {
            var obj = new WalletSummaryViewModel();
            var objWallet = _context.tblAccount_Main.FirstOrDefault(e => e.UsrNo == Usrno);
            if (objWallet != null)
            {
                obj.Balance = objWallet.Balance.ToString();
                obj.TotalCredit = objWallet.Credit.ToString();
                obj.TotalDebit = objWallet.Debit.ToString();
                obj.TotalTransaction = _context.tblAccount_MainDetails.Count(e => e.UsrNo == Usrno).ToString();
            }
            return obj;
        }
        public List<WalletStatementModel> WalletStatements(SearchModel model)
        {
            List<WalletStatementModel> list = new List<WalletStatementModel>();
            var objStatement = _context.tblAccount_MainDetails
                               .Where(e => e.UsrNo == model.Usrno)
                               .ToList();

            if (!string.IsNullOrEmpty(model.SearchText))
                objStatement = objStatement
                    .Where(e => e.TransactionId.Contains(model.SearchText))
                    .ToList();

            if (!string.IsNullOrEmpty(model.FromDate))
            {
                DateTime fromdate = DateTime.ParseExact(
                    model.FromDate,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture
                );

                objStatement = objStatement
                    .Where(e => e.AddDate >= fromdate)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(model.ToDate))
            {
                DateTime txttoDate = DateTime.ParseExact(
                    model.ToDate,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture
                );

                // Include full day till 11:59 PM
                txttoDate = txttoDate.AddDays(1).AddSeconds(-1);

                objStatement = objStatement
                    .Where(e => e.AddDate <= txttoDate)
                    .ToList();
            }
            if (objStatement.Count > 0)
            {
                list = objStatement.Select(e => new WalletStatementModel
                {
                    TransactionId = e.TransactionId,
                    Amount = e.Amount.ToString(),
                    Factor = e.Factor,
                    Narration = e.Narration,
                    Remark = e.Remark,
                    TransactionDate = e.AddDate.ToString(),
                    Balance = e.Balance.ToString()
                }).ToList();
            }
            return list;
        }
        public FundRequestSummary FundRequestSummary(int Usrno)
        {
            var obj = new FundRequestSummary();
            var totalRequests = _context.tblHistory_FundRequest
                                .Where(e => e.Usrno == Usrno)
                                .ToList();
            obj.TotalAmount = totalRequests.Sum(e => e.Amount).ToString();
            obj.PendingAmount = totalRequests
                                .Where(e => e.IsApproved == false && e.IsRejected == false)
                                .Sum(e => e.Amount).ToString();
            obj.RejectAmount = totalRequests.Where(e => e.IsRejected == true)
                                .Sum(e => e.Amount).ToString();
            obj.ApproveAmount = totalRequests.Where(e => e.IsApproved == true).Sum(e => e.Amount).ToString();
            return obj;
        }
        public JsonResponse FundRequest(FundRequestModel model)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                if (model.UploadProof != null && model.UploadProof.ContentLength > 0)
                {
                    response = FileUploadHelper.FileUpload(model.UploadProof, "FundRequest", model.UTRNo);
                    if (response.status == 0)
                        return response;
                    model.TransactionProof = response.message;
                }
                var IsUTRExist = _context.tblHistory_FundRequest.FirstOrDefault(e => e.UTRNo == model.UTRNo);
                if (IsUTRExist != null)
                {
                    response.status = 0;
                    response.message = "This UTR No. is already used in previous fund request.";
                    return response;
                }
                tblHistory_FundRequest newRequest = new tblHistory_FundRequest
                {
                    Usrno = SessionHelper.Usrno,
                    Amount = Convert.ToDecimal(model.Amount),
                    UTRNo = model.UTRNo,
                    TransactionDate = Convert.ToDateTime(model.TransactionDate),
                    PaymentMode = model.PaymentMode,
                    BankName = model.BankName,
                    TransactionProof = model.TransactionProof,
                    AddDate = DateTime.Now,
                    IsApproved = false,
                    IsRejected = false,
                };
                _context.tblHistory_FundRequest.Add(newRequest);
                _context.SaveChanges();
                response.status = 1;
                response.message = "Fund request submitted successfully.";
            }
            catch (Exception ex)
            {
                response.status = 0;
                response.message = ex.Message;
            }
            return response;
        }
        public List<FundRequestListViewModel> FundRequestList(SearchModel model)
        {
            var query =
                from fr in _context.tblHistory_FundRequest
                join u in _context.tblMaster_User
                    on fr.Usrno equals u.Usrno
                where (fr.Usrno == model.Usrno || (model.Usrno) == 0)
                select new { fr, u };

            // 🔍 Search Filter (DB level)
            if (!string.IsNullOrEmpty(model.SearchText))
            {
                query = query.Where(x => x.fr.UTRNo.Contains(model.SearchText));
            }

            // 📅 From Date (DB level)
            if (!string.IsNullOrEmpty(model.FromDate))
            {
                DateTime fromdate = DateTime.ParseExact(
                    model.FromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                query = query.Where(x => x.fr.AddDate >= fromdate);
            }

            // 📅 To Date (DB level)
            if (!string.IsNullOrEmpty(model.ToDate))
            {
                DateTime toDate = DateTime.ParseExact(
                    model.ToDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                    .AddDays(1).AddSeconds(-1);

                query = query.Where(x => x.fr.AddDate <= toDate);
            }

            // ✅ Final projection (single DB hit only here)
            var list = query
                .OrderByDescending(x => x.fr.AddDate)
                .Select(x => new FundRequestListViewModel
                {
                    id = x.fr.ID,
                    PaymentMode = x.fr.PaymentMode,
                    TransactionDate = x.fr.TransactionDate.ToString(),
                    Amount = x.fr.Amount.ToString(),
                    UTRNo = x.fr.UTRNo,
                    BankName = x.fr.BankName,
                    TransactionProof = x.fr.TransactionProof,
                    Remarks = x.fr.Remark,
                    RequestDate = (DateTime)x.fr.AddDate,
                    Status = x.fr.IsApproved == true
                                ? "Approved"
                                : (x.fr.IsRejected == true ? "Rejected" : "Pending"),

                    AgentName = x.u.Name,
                    AgentUserId = x.u.UserID,
                    AgentEmail = x.u.Email
                })
                .ToList();

            return list;
        }

        public List<SelectListItem> CreditType()
        {
            var CreditType = _context.tblMaster_CreditType.ToList();
            var CreditTypeList = CreditType.Select(x => new SelectListItem
            {
                Text = x.CreditType,
                Value = x.CreditTypeId.ToString()
            }).ToList();
            return CreditTypeList;
        }
        public string GetCreditLimit(int Usrno)
        {
            var balance = "0.00";
            if (Usrno > 0)
            {
                balance = _context.CreditWallets.FirstOrDefault(e => e.UsrNo == Usrno).Balance.ToString();
            }
            return balance;
        }
        public JsonResponse CreditLimitRequest(CreditLimitRequestModel model)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                tblManage_CreditLimit dbCreditLimit = new tblManage_CreditLimit();
                dbCreditLimit.Usrno = SessionHelper.Usrno;
                dbCreditLimit.CreditTypeID = model.CreditTypeID;
                dbCreditLimit.Amount = model.Amount;
                dbCreditLimit.RequestedAmount = model.Amount;
                dbCreditLimit.Remark = model.Reason;
                dbCreditLimit.AttachFile = model.AttachFile;
                dbCreditLimit.IsApproved = false;
                dbCreditLimit.IsRejected = false;
                dbCreditLimit.AddDate = DateTime.Now;
                _context.tblManage_CreditLimit.Add(dbCreditLimit);
                _context.SaveChanges();
                response.status = 1;
                response.message = "Request Successfully Submited";

            }
            catch (Exception ex)
            {
                response.status = 0;
                response.message = ex.Message;
            }
            return response;
        }
        public List<CreditLimitRequestListViewModel> CreditLimitRequestList(SearchModel model)
        {
            var list = new List<CreditLimitRequestListViewModel>();

            var query =
                from fr in _context.tblManage_CreditLimit
                join u in _context.tblMaster_User
                    on fr.Usrno equals u.Usrno
                where (fr.Usrno == model.Usrno || (model.Usrno) == 0)
                select new { fr, u };
            // 🔍 Search Filter (DB level)
            if (!string.IsNullOrEmpty(model.SearchText))
            {
                query = query.Where(x => x.u.UserID.Contains(model.SearchText));
            }

            // 📅 From Date (DB level)
            if (!string.IsNullOrEmpty(model.FromDate))
            {
                DateTime fromdate = DateTime.ParseExact(
                    model.FromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                query = query.Where(x => x.fr.AddDate >= fromdate);
            }

            // 📅 To Date (DB level)
            if (!string.IsNullOrEmpty(model.ToDate))
            {
                DateTime toDate = DateTime.ParseExact(
                    model.ToDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                    .AddDays(1).AddSeconds(-1);

                query = query.Where(x => x.fr.AddDate <= toDate);
            }

            // ✅ Final projection (single DB hit only here)

            if(query.Count() == 0)
                return list;

            list = query
                .OrderByDescending(x => x.fr.AddDate)
                .Select(x => new CreditLimitRequestListViewModel
                {
                    id = x.fr.ID,
                    CreditType = (_context.tblMaster_CreditType.FirstOrDefault(e=>e.CreditTypeId == x.fr.CreditTypeID).CreditType),
                    Amount = x.fr.Amount.ToString(),
                    Remarks = x.fr.Remark,
                    RequestDate = (DateTime)x.fr.AddDate,
                    Status = x.fr.IsApproved == true
                                ? "Approved"
                                : (x.fr.IsRejected == true ? "Rejected" : "Pending"),

                    AgentName = x.u.Name,
                    AgentUserId = x.u.UserID,
                    AgentEmail = x.u.Email
                })
                .ToList();
            return list;
        }
        public CreditLimitSummaryViewModel CreditLimitSummary(int Usrno)
        {
            var obj = new CreditLimitSummaryViewModel();
            var totalRequests = _context.tblManage_CreditLimit
                                .Where(e => e.Usrno == Usrno)
                                .ToList();
            obj.TotalLimit = totalRequests.Sum(e => e.Amount).ToString();
            obj.PendingLimit = totalRequests
                                .Where(e => e.IsApproved == false && e.IsRejected == false)
                                .Sum(e => e.Amount).ToString();
            obj.UnbilledAmount = totalRequests.Sum(e => e.RequestedAmount).ToString();
            obj.DueBillAmount = totalRequests
                                .Where(e => e.IsApproved == true)
                                .Sum(e => e.RequestedAmount).ToString();

            return obj;
        }

        public async Task<string> PaymentGetwayCreateOrder(int usrno, decimal amount)
        {
            var request = new
            {
                Usrno = usrno,
                Amount = amount,
                GatewayType = 1 // Razorpay
            };
            string token = GenerateToken(usrno);
            _api.SetBearerToken(token);
            dynamic orderDetails = await _api.PostAsync<dynamic>("Payment/create-order", request);

            string orderId = orderDetails.orderId;
            return orderId;
           // throw new NotImplementedException();
        }

        public async Task<dynamic> PaymentGetwayVerifyPaymentAsync(int usrno,string paymentId, string orderId, string signature)
        {
            var request = new
            {
                OrderId = orderId,
                PaymentId = paymentId,
                Signature = signature,
                GatewayType = 1 // Razorpay
            };
            string token = GenerateToken(usrno);
            _api.SetBearerToken(token);
            dynamic orderDetails = await _api.PostAsync<dynamic>("Payment/verify", request);
            return orderDetails;
        }
        public string GenerateToken(int usrno)
        {
            string cacheKey = $"ApiToken_{usrno}";
            if (AppCache.Exists(cacheKey))
                return AppCache.Get<string>(cacheKey);

            string token = string.Empty;
            var objUser = _context.tblMaster_User.FirstOrDefault(e => e.Usrno == usrno);
            if (objUser != null)
            {
                var playload = new
                {
                    userId = objUser.Email,
                    password = objUser.Password,
                    userType = "B2B"
                };
                string url = "Auth/GetToken";
                var apiresponse = _api.Post<JsonResponse>(url, playload);
                if (apiresponse.status > 0)
                {
                    token = Convert.ToString(apiresponse.data.token);
                    AppCache.Set(cacheKey, token, 40); // 40 minutes
                }
            }
            return token;
        }
    }
}