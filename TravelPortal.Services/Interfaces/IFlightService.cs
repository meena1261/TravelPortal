using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPortal.Models;
using TravelPortal.Models.Amadeus;
using TravelPortal.Models.DTOs;
using TravelPortal.Models.Json;

namespace TravelPortal.Services.Interfaces
{
    public interface IFlightService
    {
        JsonResponse GetAirportsAsync(string keyword);
        Task<JsonResponse> SearchFlightsAsync(SearchRequestDto request);
        Task<JsonResponse> FlightDetails(FlightDetailsRequestDto request);
        Task<JsonResponse> FlightSeats(FlightDetailsRequestDto request);
        Task<JsonResponse> BookingCreate(CreateFlightOrderDTO request,int UserId,string Role);
    }
}
