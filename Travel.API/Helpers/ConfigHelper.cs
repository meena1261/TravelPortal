using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.API.Helper
{
    public static class ConfigHelper
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string ConnectionString =>
            _configuration.GetConnectionString("DefaultConnection");

        public static string CacheExpiryLimit =>
            _configuration["CacheExpiryLimit"];
        
    }
}
