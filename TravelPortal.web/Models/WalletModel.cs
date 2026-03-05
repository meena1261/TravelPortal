using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models
{
    public class DebitCreditFundModel
    {
        public int Usrno { get; set; }
        public string Factor { get; set; }
        public string Amount { get; set; }
        public string Remark { get; set; }
    }
    public class CreditDebitModel
    {
        public int Usrno { get; set; }
        public decimal Amount { get; set; }
        public string Factor { get; set; }
        public string Narration { get; set; }
        public string Remark { get; set; }
        public string TransactionId { get; set; }
    }
    public class WalletSummaryViewModel
    {
        public string Balance { get; set; }
        public string TotalCredit { get; set; }
        public string TotalDebit { get; set; }
        public string TotalTransaction { get; set; }

    }
    public class FundRequestModel
    {
        public int Usrno { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionDate { get; set; }
        public string Amount { get; set; }
        public string UTRNo { get; set; }
        public string BankName { get; set; }
        public string TransactionProof { get; set; }
        public HttpPostedFileBase UploadProof { get; set; }
    }
    
    public class FundRequestSummary
    {
        public string TotalAmount { get; set; }
        public string PendingAmount { get; set; }
        public string RejectAmount { get; set; }
        public string ApproveAmount { get; set; }

    }
    public class FundRequestActionModel
    {
        public string Action { get; set; }
        public int Id { get; set; }
        public string Reason { get; set; }
    }
    public class WalletStatementModel
    {
        public string TransactionDate { get; set; }
        public string Amount { get; set; }
        public string Factor { get; set; }
        public string Balance { get; set; }
        public string Narration { get; set; }
        public string Remark { get; set; }
        public string TransactionId { get; set; }
    }
    public class CreditLimitRequestModel
    {
        public int Usrno { get; set; }
        public int CreditTypeID { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public string AttachFile { get; set; }
    }
    public class CreditLimitRequestListModel
    {
        public decimal Amount { get; set; }
        public int CreditType { get; set; }
        public string AdminReason { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
    }
    public class CreditLimitSummaryViewModel
    {
        public string TotalLimit { get; set; }
        public string PendingLimit { get; set; }
        public string UnbilledAmount { get; set; }
        public string DueBillAmount { get; set; }

    }
}