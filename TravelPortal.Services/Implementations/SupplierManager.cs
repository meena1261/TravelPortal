using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPortal.Data.Context;
using TravelPortal.Data.Entities;

namespace TravelPortal.Services.Implementations
{
    public class SupplierManager
    {
        private readonly TravelDbContext _context;

        public SupplierManager(TravelDbContext context)
        {
            _context = context;
        }

        public async Task<List<TblManageApi>> GetActiveSuppliersAsync()
        {
            return await _context.TblManageApis
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.Priority)
                .ToListAsync();
        }

        public async Task<TblManageApi> GetByNameAsync(string name)
        {
            return await _context.TblManageApis.FirstAsync(x => x.Supplier == name);
        }
    }
}
