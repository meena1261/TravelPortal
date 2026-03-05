using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.web.Models;
using TravelPortal.web.Models.Common;

namespace TravelPortal.web.Helpers
{
    public class AdoRepository
    {
        public static JsonResponse AddEditUser(AddEditUserModel model)
        {
            JsonResponse response = new JsonResponse();

            response = Connection.Query<JsonResponse>($"Exec ProcMaster_User 'AddEditUser','{model.Usrno}','{model.Mobile}',N'{model.Name}','{model.Email}','{model.AspNetID}','{model.UserID}','{model.Password}','{model.ReferralCode}','{model.CreatedByUserID}','{model.UserTypeID}'").FirstOrDefault();
            return response;
        }
        public static List<T> Report<T>(string type, int usrno = 0, string fromDate = "", string toDate = "", string DateRange = "", string userId = "", string status = "", int IntroUsrno = 0)
        {
            if (!string.IsNullOrEmpty(DateRange))
            {
                var dates = DateRange.Split('-');
                fromDate = dates[0].Trim();
                toDate = dates[1].Trim();
            }

            DynamicParameters param = new DynamicParameters();
            param.Add("@Action", type);
            param.Add("@LoginUsrno", usrno);
            param.Add("@fromDate", fromDate);
            param.Add("@toDate", toDate);
            param.Add("@userId", userId);
            param.Add("@status", status);
            param.Add("@IntroUsrno", IntroUsrno);

            return Connection.Query<T>("Proc_Report", param).ToList();

        }
        public static JsonResponse MainWalletCreditDebit(int Usrno, decimal Amount, string Factor, string Narration = "", string Remark = "", string TransactionId = "")
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@UsrNo", Usrno);
            param.Add("@Amount", Amount);
            param.Add("@Factor", Factor);
            param.Add("@Narration", Narration);
            param.Add("@IsReturn", 1);
            param.Add("@Remark", Remark);
            param.Add("@TransactionId", TransactionId);

            var result = Connection.ReturnList<JsonResponse>("ProcMaster_MainWallet", param).FirstOrDefault();
            return result;
        }
        public static JsonResponse CreditWalletCreditDebit(int Usrno, decimal Amount, string Factor, string Narration = "", string Remark = "", string TransactionId = "")
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@UsrNo", Usrno);
            param.Add("@Amount", Amount);
            param.Add("@Factor", Factor);
            param.Add("@Narration", Narration);
            param.Add("@IsReturn", 1);
            param.Add("@Remark", Remark);
            param.Add("@TransactionId", TransactionId);

            var result = Connection.ReturnList<JsonResponse>("ProcMaster_CreditWallet", param).FirstOrDefault();
            return result;
        }
    }
}