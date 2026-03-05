using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models
{
    public class RequestCreditLimitModel
    {
        public int Usrno { get; set; }
        public int CreditTypeID { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public string AttachFile { get; set; }
    }
    public class RequestCreditLimitListModel
    {
        public decimal Amount { get; set; }
        public int CreditType { get; set; }
        public string AdminReason { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
    }
    public class AccountActivityModel : PasswordModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string VerificationCode { get; set; }
        public bool IsActive { get; set; }
    }
    public class GetByUsrno
    {
        public int Usrno { get; set; }
    }
    public class CommanTypeModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class UserTypeModel
    {
        public int ID { get; set; }
        public string UserType { get; set; }
        public int CreatedByUsrno { get; set; }
    }
    public class AddEditUserModel : PasswordModel
    {
        public int Usrno { get; set; }
        public string UserTypeID { get; set; }
        public string UserType { get; set; }
        public string AspNetID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string ReferralCode { get; set; }
        public string RegisterOTP { get; set; }
        public int CreatedByUserID { get; set; }
        public bool? IsKycCompleted { get; set; }

    }
    public class RegisterByOTPModel : PasswordModel
    {
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }
        public string OTP { get; set; }
        public int IsValid { get; set; }
        public int CurrentIndex { get; set; }
    }
    public class AgentKycModel
    {
        public int Usrno { get; set; }
        public string AgencyName { get; set; }
        [RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Invalid PAN Number")]
        public string PanNumber { get; set; }
        public string PancardImg { get; set; }
        public string GSTNumber { get; set; }
        public string AadhaarNumber { get; set; }
        public int AgencyTypeID { get; set; }
        public int BusinessTypeID { get; set; }
        public string AgencyAddress { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int KYCDocumentTypeID { get; set; }
        public string DocumentFile { get; set; }
        public int SupportingDocumentTypeID { get; set; }
        public string SupportingDocumentFile { get; set; }
    }

    public class ApproveRejectActionModel
    {
        public string Action { get; set; }
        public int Id { get; set; }
        public string Reason { get; set; }
    }

    public class CreditLimitApproveRejectModel
    {
        public string Action { get; set; }
        public int Id { get; set; }
        public string Amount { get; set; }
        public string Days { get; set; }
        public string Reason { get; set; }
    }
    public class PasswordModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
ErrorMessage = "Password must be at least 8 characters long, contain at least one letter, one number, and one special character (@$!%*#?&).")]
        [Display(Name = "New Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class ActionInvitedUserModel
    {
        public string type { get; set; }
        public int id { get; set; }
        public string Value { get; set; }
    }
    public class PasswordResetByAdminModel
    {
        [Required(ErrorMessage = "Required")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
ErrorMessage = "Password must be at least 8 characters long, contain at least one letter, one number, and one special character (@$!%*#?&).")]
        [Display(Name = "New Password")]
        public string Password { get; set; }
    }
}