using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TravelPortal.Models.DTOs;
using TravelPortal.Services.Interfaces;

namespace TravelPortal.Services.Implementations
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        // ✅ GET only
        public T Get<T>(string key)
        {
            _cache.TryGetValue(key, out T value);
            return value;
        }

        // ✅ SET only
        public void Set<T>(string key, T value, int minutes = 5)
        {
            _cache.Set(key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes)
            });
        }

        // ✅ REMOVE
        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        // =========================
        // SAFE HASH KEY
        // =========================
        public string GenerateKey(string r)
        {            
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(r));
            return Convert.ToHexString(hash);
        }
    }
}
