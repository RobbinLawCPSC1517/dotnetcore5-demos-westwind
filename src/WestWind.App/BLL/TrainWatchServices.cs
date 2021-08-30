using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WestWind.Entities;
using WestWind.DAL;



namespace WestWind.Services 
{
    public class TrainWatchServices 
    {
        private readonly WestWindContext _context;
        public TrainWatchServices(WestWindContext context) {
            _context = context ?? throw new ArgumentNullException();
        }

        #region QUERY
        public BuildVersion GetDbVersion() 
        {
            Console.WriteLine($"TrainWatchServices: GetDbVersion;");
            var result = _context.BuildVersion.ToList();
            return result.First();
        }
        
        // Returns all RailCarType records.
        public List<RailCarType> ListRailCarTypes()
        {
            Console.WriteLine($"TrainWatchServices: ListRailCarTypes;");
            return _context.RailCarTypes.ToList();
        }

        // Returns zero or more RollingStock records containing the supplied partial Reporting Mark string.
        public List<RollingStock> FindRollingStocksByPartialReportingMark(string partialReportingMark)
        {
            Console.WriteLine($"TrainWatchServices: FindRollingStocksByPartialReportingMark; partialReportingMark= {partialReportingMark}");
            return _context.RollingStock.Where(x=>x.ReportingMark.Contains(partialReportingMark)).ToList();
        }

        // Returns zero or more RollingStock records matching the supplied railCarTypeId
        public List<RollingStock> FindRollingStocksByRailCarType(int? railCarTypeId)
        {
            Console.WriteLine($"TrainWatchServices: FindRollingStocksByRailCarType; railCarTypeId= {railCarTypeId}");
            return _context.RollingStock.Where(x=>x.RailCarTypeId == railCarTypeId).ToList();
        }        
        #endregion
        
        #region READ - Retrieve, Edit, Add, Delete
        public RollingStock Retrieve(string reportingMark)
        {
            Console.WriteLine($"TrainWatchServices: Retrieve; reportingMark= {reportingMark}");
            return _context.RollingStock.Find(reportingMark);
        }

        public void Edit(RollingStock item)
        {
            Console.WriteLine($"TrainWatchServices: Edit; reportingMark= {item.ReportingMark}");
            var existing = _context.Entry(item);
            existing.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Add(RollingStock item)
        {
            Console.WriteLine($"TrainWatchServices: Add; reportingMark= {item.ReportingMark}");
            _context.RollingStock.Add(item);
            _context.SaveChanges();
        }

        public void Delete(RollingStock item)
        {
            Console.WriteLine($"TrainWatchServices: Delete; reportingMark= {item.ReportingMark}");
            var existing = _context.Entry(item);
            existing.State = EntityState.Deleted;
            _context.SaveChanges();
        }
        #endregion
    }
}