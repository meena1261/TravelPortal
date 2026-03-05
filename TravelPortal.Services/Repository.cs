using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.API.Data;
using TravelPortal.Models;
using TravelPortal.Models.Ado;
using TravelPortal.Models.DTOs;
using TravelPortal.Models.Enums;
using TravelPortal.Models.Json;
using TravelPortal.Services.Interfaces;

namespace TravelPortal.Services
{
    public class Repository: IRepository
    {
        private readonly ITravelPortalAPIDB _connection;
        public Repository(ITravelPortalAPIDB connection) { _connection = connection; }
        
        public JsonResponse Login(LoginDto model)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                response = _connection.Query<JsonResponse>($"EXEC Proc_Login '{model.UserType}','{model.UserId}','{model.Password}'").FirstOrDefault();
            }
            catch 
            {
                response.Status = 0;
                response.Message = "Invalid Credintional";
            }
            return response;
        }
        public JsonResponse LoginViaOTP(LoginViaOtpModel login)
        {
            JsonResponse response = new JsonResponse();

            response = _connection.Query<JsonResponse>($"Exec ProcManage_Customer 'Insert',0,'','{login.Mobile}'").FirstOrDefault();
            //if (response.Status == 2) // Send OTP
            //{

            //}
            //else if (response.Status == 1)
            //{
            //    response.data = new { Usrno = Convert.ToInt32(response.data) };
            //}

            return response;
        }
        public JsonResponse GetProfile(GetByUsrno login)
        {
            JsonResponse response = new JsonResponse();
            var obj = _connection.Query<dynamic>($"Exec ProcManage_Customer 'GetById','{login.Usrno}'").FirstOrDefault();
            if (obj != null)
            {
                response.Status = 1;
                response.Message = "Success";
                response.data = obj;
            }
            else
            {
                response.Status = 0;
                response.Message = "Invalid User";
            }
            return response;
        }
        public List<CityAirportList> Airports()
        {
            try
            {
                var list = _connection.Query<CityAirportList>("select ID as AirportId,Name as Airport,Code as IATACode,ICAOCode,City as CityName  from tblManage_CityAirport").ToList();
                if (list != null)
                {
                    return list;
                }
            }
            catch
            {
                return new List<CityAirportList>();
            }
            return new List<CityAirportList>();
        }
        public List<MarkupModel> MarkupAdd()
        {
            try
            {
                var list = _connection.Query<MarkupModel>("select MarkupTypeID as MarkupTypeId,Value as MarkupPrice,AirlineCode as Airline from tblManage_Markup where Usrno = 0").ToList();
                if (list != null)
                {
                    return list;
                }
            }
            catch
            {
                return new List<MarkupModel>();
            }
            return new List<MarkupModel>();
        }

        public JsonResponse AddEditFlightBookingHistory(AddEditFlightBookingHistory model)
        {
            var response = new JsonResponse();
            try
            {
                string paymentStatus = "PENDING";
                string BookingStatus = "PENDING";
                if (model.UserType.Equals("B2B") && model.Usrno > 0)
                {
                    decimal walletBalance = _connection.ExecuteScalar<decimal>($"select Balance from tblAccount_Main where Usrno = '{model.Usrno}'");
                    decimal CreditLimit = _connection.ExecuteScalar<decimal>($"select Balance from CreditWallet where Usrno = '{model.Usrno}'");

                    decimal walletUsed = 0;
                    decimal creditUsed = 0;
                    decimal payable = model.TotalAmount;

                    // 1️⃣ Use wallet first
                    if (walletBalance > 0 && model.PaymentMode.Equals(PaymentModeEnum.Wallet.ToString()))
                    {
                        walletUsed = Math.Min(walletBalance, payable);
                        payable -= walletUsed;

                        decimal debitAmount = 0 - walletUsed;
                        string narration = "Fund Deduct deu to PNR Generated : "+model.FlightOrderID;
                        string remark = $"PNR Generated : {model.FlightOrderID} due to sector : {model.Sector}";
                        string transactionId = "FL" + Guid.NewGuid().ToString("N").Substring(0, 12);
                        _connection.Execute($"Exec ProcMaster_MainWallet '{model.Usrno}','{debitAmount}','Dr','{narration}',0,'{remark}','{transactionId}'");
                    }

                    // 2️⃣ Then use credit
                    if (payable > 0 && CreditLimit > 0 && model.PaymentMode.Equals(PaymentModeEnum.Credit.ToString()))
                    {
                        creditUsed = Math.Min(CreditLimit, payable);
                        payable -= creditUsed;
                    }

                    model.UseWalletAmount = walletUsed;
                    model.UserCreditLimit = creditUsed;
                    model.PaybleAmount = payable;

                    if (model.PaybleAmount == 0)
                    {
                        paymentStatus = "SUCCESS";
                        BookingStatus = "COMPLETE";
                    }
                    else
                    {
                        paymentStatus = "PENDING"; // gateway payment required
                        BookingStatus = "HOLD";
                    }

                }

                _connection.Execute(@"
INSERT INTO FlightBookingHistory
(Usrno,UserType,TripType,Sector,DepartureDates,ArrivalDates,Adults,Childs,Infants,CabinClass,FlightOrderID,QueuingOfficeId,PaymentMode,TotalAmount,UseWalletAmount,UserCreditLimit,PaybleAmount,PaymentStatus,BookingStatus,AddDate)
VALUES
(@Usrno,@UserType,@TripType,@Sector,@DepartureDates,@ArrivalDates,@Adults,@Childs,@Infants,@CabinClass,@FlightOrderID,@QueuingOfficeId,@PaymentMode,@TotalAmount,@UseWalletAmount,@UserCreditLimit,@PaybleAmount,@PaymentStatus,@BookingStatus,GETDATE())
", new
                {
                    model.Usrno,
                    model.UserType,
                    model.TripType,
                    model.Sector,
                    model.DepartureDates,
                    model.ArrivalDates,
                    model.Adults,
                    model.Childs,
                    model.Infants,
                    model.CabinClass,
                    model.FlightOrderID,
                    model.QueuingOfficeId,
                    model.PaymentMode,
                    model.TotalAmount,
                    model.UseWalletAmount,
                    model.UserCreditLimit,
                    model.PaybleAmount,
                    paymentStatus,
                    BookingStatus
                });

                response.Status = 1;
                response.Message = "Booking saved successfully.";
                response.data = new
                {
                    flightOrderId = model.FlightOrderID,
                    QueuingOfficeId = model.QueuingOfficeId,
                    TotalPrice = model.TotalAmount,
                    PaybleAmount = model.PaybleAmount
                };
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Message = "Error saving booking: " + ex.Message;
            }
            return response;
        }
    }
}
