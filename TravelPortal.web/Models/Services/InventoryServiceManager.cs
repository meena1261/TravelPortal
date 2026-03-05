using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using TravelPortal.EDMX;
using TravelPortal.web.Helpers;
using TravelPortal.web.Models.Common;
using TravelPortal.web.Models.Enum;
using TravelPortal.web.Models.Interface;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Models.Services
{
    public class InventoryServiceManager : IInventoryServiceManager
    {
        private readonly db_silviEntities _context;
        public InventoryServiceManager() { _context = new db_silviEntities(); }
        public JsonResponse BecomeSupplier(BecomeSupplierModel model)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                var objUser = _context.tblMaster_User.FirstOrDefault(e => e.Usrno == model.Usrno);
                string folderpath = $"{objUser.AspNetID}/SupplierAgreements";
                response = FileUploadHelper.FileUpload(model.SupplierAgreementFile, folderpath, "Supplier_Agreement_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                if (response.status == 1)
                {
                    //Update user record
                    objUser.IsSupplier = false;
                    objUser.AgreementRemark = "Your Request Under Process";
                    objUser.SupplierAgreement = response.message;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                response.status = 0;
                response.message = "Error uploading file: " + ex.Message;
            }
            return response;
        }
        public JsonResponse AddEditInventoryService(FlightInventoryServiceModel model)
        {
            JsonResponse response = new JsonResponse();
            try
            {

                tblFlightInventory newInventory = new tblFlightInventory();
                newInventory.Usrno = SessionHelper.Usrno;
                newInventory.FlightNumber = model.FlightNumber;
                newInventory.AirlineCode = model.AirlineCode;

                DateTime fromDate;
                DateTime toDate;
                var formats = new[] { "dd-MM-yyyy", "d-M-yyyy", "yyyy-MM-dd" }; // include any client variants

                if (!DateTime.TryParseExact(model.FromDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate)
                 || !DateTime.TryParseExact(model.ToDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out toDate))
                {
                    response.status = 0;
                    response.message = "Invalid date format. Expected dd-MM-yyyy.";
                    return response;
                }

                newInventory.FromDate = fromDate;
                newInventory.Todate = toDate;
                newInventory.OriginCode = model.OriginCode;
                newInventory.DepartureTime = (TimeSpan)model.DepartureTime;
                newInventory.DepartureTerminal = model.DepartureTerminal;
                newInventory.DestinationCode = model.DestinationCode;
                newInventory.ArrivalTime = (TimeSpan)model.ArrivalTime;
                newInventory.ArrivalTerminal = model.ArrivalTerminal;
                newInventory.Email = model.Email;
                newInventory.Mobile = model.Mobile;
                newInventory.FareRules = model.FareRules;
                newInventory.ConnectionType = model.ConnectionTypes;
                newInventory.Stops = string.IsNullOrEmpty(model.FlightStops) ? 0 : Convert.ToInt32(model.FlightStops);
                newInventory.FareDisplayType = model.FareType == "" ? 1 : 2;
                newInventory.StopTicketingDays = Convert.ToInt32(model.StopTicketingBefore);
                newInventory.StopTicketingTime = model.StopTicketingTime;
                newInventory.AvailableDays = model.SelectedDays;
                newInventory.AddDate = DateTime.Now;
                newInventory.IsActive = false;
                newInventory.IsDelete = false;
                _context.tblFlightInventories.Add(newInventory);
                _context.SaveChanges();
                if (model.FlightDetails.Count > 0)
                {
                    foreach (var f in model.FlightDetails)
                    {
                        tblFlightFare newfare = new tblFlightFare();
                        newfare.FlightInventoryID = newInventory.FlightInventoryID;
                        newfare.ClassType = f.Class;
                        newfare.RBD = f.RBD;
                        newfare.Seats = (int)f.Seat;
                        newfare.AdultFare = f.AdultFare;
                        newfare.AdultFareBreakup = f.AdultFareBrackup;
                        newfare.AdultFare = f.ChildFare;
                        newfare.AdultFareBreakup = f.ChildFareBrackup;
                        newfare.AdultFare = f.InfantFare;
                        newfare.AdultFareBreakup = f.InfantFareBrackup;
                        newfare.IsBaggage = f.IsBaggageIncluded == "1" ? true : false;
                        newfare.AdultBaggage = f.AdultBaggage;
                        newfare.ChildBaggage = f.ChildBaggage;
                        newfare.InfantBaggage = f.InfantBaggage;
                        newfare.Unit = f.UnitType;
                        newfare.IsRefund = f.IsRefundable == "1" ? true : false;
                        newfare.PNR = f.PNR;
                        newfare.IsActive = true;
                        newfare.AddDate = DateTime.Now;
                        _context.tblFlightFares.Add(newfare);
                        _context.SaveChanges();
                    }
                }
                if (model.flightConnections.Count > 0 && !string.IsNullOrEmpty(model.ConnectionTypes) && !string.IsNullOrEmpty(model.FlightStops))
                {
                    foreach (var f in model.flightConnections)
                    {
                        tblFlightConnectionVia connectionVia = new tblFlightConnectionVia();
                        connectionVia.FlightInventoryID = newInventory.FlightInventoryID;
                        connectionVia.Operator = f.Operator;
                        connectionVia.Flight = f.Flight;
                        connectionVia.FlightNo = f.FlightNo;
                        connectionVia.OriginCode = f.OriginCode;
                        connectionVia.DepatureTime = f.DepatureTime;
                        connectionVia.DepatureTerminal = f.DestinationTerminal;
                        connectionVia.DestinationCode = f.DestinationCode;
                        connectionVia.ArrivalTime = f.ArrivalTime;
                        connectionVia.ArrivalTerminal = f.DestinationTerminal;
                        connectionVia.AddDate = DateTime.Now;
                        connectionVia.IsActive = false;
                        connectionVia.IsDelete = false;
                        _context.tblFlightConnectionVias.Add(connectionVia);
                        _context.SaveChanges();
                    }
                }

                response.status = 1;
                response.message = "Successfully Added";
            }
            catch (Exception ex)
            {
                response.status = 0;
                response.message = "same thing went wrong";
            }
            return response;
        }
        public List<InventoryListViewModel> ListInventory(SearchModel model)
        {
            var list = new List<InventoryListViewModel>();
            var inventories = _context.tblFlightInventories.Where(e => e.Usrno == model.Usrno || (model.Usrno) == 0).ToList();
            if (inventories.Count > 0)
            {
                var objAirport = PreloadApplicationData.Cities;
                foreach (var i in inventories)
                {
                    int OriginId = Convert.ToInt32(i.OriginCode);
                    int DestinationId = Convert.ToInt32(i.DestinationCode);

                    string fromCity = objAirport.FirstOrDefault(e => e.CityAirportId == OriginId).IATACode;
                    string ToCity = objAirport.FirstOrDefault(e => e.CityAirportId == DestinationId).IATACode;

                    InventoryListViewModel obj = new InventoryListViewModel();
                    obj.InvestorId = i.Usrno;
                    obj.FlightNo = i.AirlineCode;
                    obj.Sector = fromCity + "-" + ToCity;
                    obj.DeptTime = i.DepartureTime.ToString();
                    obj.ArrivalTime = i.ArrivalTime.ToString();
                    obj.From = fromCity;
                    obj.To = ToCity;
                   // obj.BlackoutDays = i.BlackoutDays;
                    obj.DaysofOperations = i.AvailableDays;
                    obj.Supplier = i.Email;
                    var fares = _context.tblFlightFares.Where(e => e.FlightInventoryID == i.FlightInventoryID).ToList();
                    if (fares.Count > 0)
                    {
                        obj.TotalEconomy = (int)fares.Where(e => e.ClassType == EnumFlightClassType.Economy.ToString()).Sum(e => e.Seats);
                        obj.TotalFirstClass = (int)fares.Where(e => e.ClassType == EnumFlightClassType.FIRST.ToString()).Sum(e => e.Seats);
                        obj.TotalBussiness = (int)fares.Where(e => e.ClassType == EnumFlightClassType.BUSINESS.ToString()).Sum(e => e.Seats);
                    }
                    obj.Status = i.IsActive == true ? "Active" : "Inactive";
                    list.Add(obj);
                }
                return list;
            }
            return new List<InventoryListViewModel>();
        }
    }
}