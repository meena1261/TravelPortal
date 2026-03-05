using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace TravelPortal.web.Helpers
{
    public static class AppCache
    {
        private static MemoryCache cache = MemoryCache.Default;

        public static T Get<T>(string key)
        {
            return (T)cache.Get(key);
        }

        public static void Set(string key, object data, int minutes)
        {
            cache.Set(key, data, DateTimeOffset.Now.AddMinutes(minutes));
        }

        public static bool Exists(string key)
        {
            return cache.Contains(key);
        }
    }
}