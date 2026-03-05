using Microsoft.AspNetCore.Mvc;
using Travel.API.Extensions;
using Travel.API.Helpers;
using TravelPortal.Models;
using TravelPortal.Models.Amadeus;
using TravelPortal.Models.DTOs;
using TravelPortal.Services.Interfaces;

namespace Travel.API.Controllers
{
    public class FlightController : BaseController
    {
        private readonly IFlightService _flightService;
        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }
        [HttpPost("Airports")]
        [ProducesResponseType(typeof(JsonResponse<List<CityAirportList>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Airports([FromBody] SearchAirport model)
        {
            var result = _flightService.GetAirportsAsync(model.Keyword);
            return Ok(result);
        }
        [HttpPost("Search")]
        [ProducesResponseType(typeof(JsonResponse<FlightResults>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Search([FromBody] SearchRequestDto request)
        {
            var result = await _flightService.SearchFlightsAsync(request);
            return Ok(result);
        }
        [HttpPost("FlightDetails")]
        [ProducesResponseType(typeof(JsonResponse<FlightDetailModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFlightDetails([FromBody] FlightDetailsRequestDto request)
        {
            var result = await _flightService.FlightDetails(request);
            return Ok(result);
        }
        [HttpPost("Seats")]
        public async Task<IActionResult> Seats([FromBody] FlightDetailsRequestDto request)
        {
            var result = await _flightService.FlightSeats(request);
            return Ok(result);
        }
        [HttpPost("PNRGenerate")]
        public async Task<IActionResult> PNRGenerate([FromBody] CreateFlightOrderDTO request)
        {
            int userId = Convert.ToInt32(User.GetUserId()); // null if no Authorize
            var userrole = User.GetRole(); // null if no Authorize
            var result = await _flightService.BookingCreate(request,userId,userrole);
            return Ok(result);
        }
    }
}
