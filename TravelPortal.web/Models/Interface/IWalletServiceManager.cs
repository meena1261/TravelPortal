using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TravelPortal.web.Models.Common;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Models.Interface
{
    public interface IWalletServiceManager
    {
        string GetWalletBalance(int Usrno);
        JsonResponse CreditDebit(CreditDebitModel model);
        WalletSummaryViewModel WalletSummary(int Usrno);
        List<WalletStatementModel> WalletStatements(SearchModel model);
        FundRequestSummary FundRequestSummary(int Usrno);
        JsonResponse FundRequest(FundRequestModel model);
        List<FundRequestListViewModel> FundRequestList(SearchModel model);
        List<SelectListItem> CreditType();
        JsonResponse CreditLimitRequest(CreditLimitRequestModel model);
        string GetCreditLimit(int Usrno);
        List<CreditLimitRequestListViewModel> CreditLimitRequestList(SearchModel model);
        CreditLimitSummaryViewModel CreditLimitSummary(int Usrno);
        Task<string> PaymentGetwayCreateOrder(int usrno,decimal amount);
        Task<dynamic> PaymentGetwayVerifyPaymentAsync(int usrno,string paymentId, string orderId, string signature);
    }
}