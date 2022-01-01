using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TrainWatch.Entities;
using TrainWatch.DAL;



namespace TrainWatch.Services 
{
    public class TrainWatchServices 
    {
        private readonly TrainWatchContext Context;
        public TrainWatchServices(TrainWatchContext context) {
            if (context == null)
                throw new ArgumentNullException();
            Context = context;
        }

        #region QUERY
        public DbVersion GetDbVersion() 
        {
            Console.WriteLine($"TrainWatchServices: GetDbVersion;");
            // var result = Context.DbVersion.ToList();
            // return result.First();
            return Context.DbVersion.ToList().First();
        }
        
        // Returns all RailCarType records.
        // public List<RailCarType> ListRailCarTypes()
        // {
        //     Console.WriteLine($"TrainWatchServices: ListRailCarTypes;");
        //     return Context.RailCarTypes.ToList();
        // }

        // Returns zero or more RollingStock records matching the supplied railCarTypeId
        // public List<RollingStock> FindRollingStocksByRailCarType(int? railCarTypeId)
        // {
        //     Console.WriteLine($"TrainWatchServices: FindRollingStocksByRailCarType; railCarTypeId= {railCarTypeId}");
        //     return Context.RollingStock.Where(x=>x.RailCarTypeId == railCarTypeId).ToList();
        // }        

        // Returns zero or more RollingStock records containing the supplied partial Reporting Mark string.
        // public List<RollingStock> FindRollingStocksByPartialReportingMark(string partialReportingMark)
        // {
        //     Console.WriteLine($"TrainWatchServices: FindRollingStocksByPartialReportingMark; partialReportingMark= {partialReportingMark}");
        //     return Context.RollingStock.Where(x=>x.ReportingMark.Contains(partialReportingMark)).ToList();
        // }
        #endregion
        
        #region READ - Retrieve, Edit, Add, Delete
        // public RollingStock Retrieve(string reportingMark)
        // {
        //     Console.WriteLine($"TrainWatchServices: Retrieve; reportingMark= {reportingMark}");
        //     return Context.RollingStock.Find(reportingMark);
        // }

        // public void Edit(RollingStock item)
        // {
        //     Console.WriteLine($"TrainWatchServices: Edit; reportingMark= {item.ReportingMark}");
        //     var existing = Context.Entry(item);
        //     existing.State = EntityState.Modified;
        //     Context.SaveChanges();
        // }

        // public void Add(RollingStock item)
        // {
        //     Console.WriteLine($"TrainWatchServices: Add; reportingMark= {item.ReportingMark}");
        //     List<RollingStock> SearchedRecords = Context.RollingStock.Where(x => x.ReportingMark.Contains(item.ReportingMark)).ToList();
        //     if(SearchedRecords.Count != 0)
        //         throw new Exception("Reporting Mark already exists. Record NOT added to database.");
        //     Context.RollingStock.Add(item);
        //     Context.SaveChanges();
        // }

        // public void Delete(RollingStock item)
        // {
        //     Console.WriteLine($"TrainWatchServices: Delete; reportingMark= {item.ReportingMark}");
        //     var existing = Context.Entry(item);
        //     existing.State = EntityState.Deleted;
        //     Context.SaveChanges();
        // }
        #endregion
    }
}