using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPortal.Models.DTOs;

namespace TravelPortal.Services.Interfaces
{
    public interface ICacheService
    {
        string GenerateKey(string r);
        T Get<T>(string key);
        void Set<T>(string key, T value, int minutes = 5);
        void Remove(string key);
    }

}
