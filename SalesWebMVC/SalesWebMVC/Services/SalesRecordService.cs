using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMVCContext _context;
        public SalesRecordService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(temp => temp.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(temp => temp.Date <= maxDate.Value);
            }

            return await result.Include(temp => temp.Seller)
                         .Include(temp => temp.Seller.Department)
                         .OrderByDescending(temp => temp.Date)
                         .ToListAsync();
        }
    }
}
