using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.API.Data;
using TravelPortal.Models;

namespace TravelPortal.Services.Interfaces
{
    // define interface
    public interface IRepository { List<CityAirportList> Airports(); }
}
