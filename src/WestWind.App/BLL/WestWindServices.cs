using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WestWind.Entities;
using WestWind.DAL;

namespace WestWind.Services 
{
    public class WestWindServices 
    {
        private readonly WestWindContext _context;
        public  WestWindServices(WestWindContext context) {
            _context = context ?? throw new ArgumentNullException();
        }

        #region QUERY
        public BuildVersion GetDbVersion() 
        {
            Console.WriteLine($"WestWindServices: GetDbVersion;");
            var result = _context.BuildVersion.ToList();
            return result.First();
        }
        
        // Returns all RailCarType records.
        public List<RailCarType> ListRailCarTypes()
        {
            Console.WriteLine($"WestWindServices: ListRailCarTypes;");
            return _context.RailCarTypes.ToList();
        }

        // Returns zero or more RollingStock records containing the supplied partial Reporting Mark string.
        public List<RollingStock> FindRollingStocksByPartialReportingMark(string partialReportingMark)
        {
            Console.WriteLine($"WestWindServices: FindRollingStocksByPartialReportingMark; partialReportingMark= {partialReportingMark}");
            return _context.RollingStock.Where(x=>x.ReportingMark.Contains(partialReportingMark)).ToList();
        }

        // Returns zero or more RollingStock records matching the supplied railCarTypeId
        public List<RollingStock> FindRollingStocksByRailCarType(int? railCarTypeId)
        {
            Console.WriteLine($"WestWindServices: FindRollingStocksByRailCarType; railCarTypeId= {railCarTypeId}");
            return _context.RollingStock.Where(x=>x.RailCarTypeId == railCarTypeId).ToList();
        }        
        #endregion
        
        #region READ - Retrieve, Edit, Add, Delete
        public RollingStock Retrieve(string reportingMark)
        {
            Console.WriteLine($"WestWindServices: Retrieve; reportingMark= {reportingMark}");
            return _context.RollingStock.Find(reportingMark);
        }

        public void Edit(RollingStock item)
        {
            Console.WriteLine($"WestWindServices: Edit; reportingMark= {item.ReportingMark}");
            var existing = _context.Entry(item);
            existing.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Add(RollingStock item)
        {
            Console.WriteLine($"WestWindServices: Add; reportingMark= {item.ReportingMark}");
            _context.RollingStock.Add(item);
            _context.SaveChanges();
        }

        public void Delete(RollingStock item)
        {
            Console.WriteLine($"WestWindServices: Delete; reportingMark= {item.ReportingMark}");
            var existing = _context.Entry(item);
            existing.State = EntityState.Deleted;
            _context.SaveChanges();
        }
        #endregion
    }
}