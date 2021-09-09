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
        
        // Returns all Category records.
        public List<Category> ListCategories()
        {
            Console.WriteLine($"WestWindServices: ListCategories();");
            return _context.Categories.ToList();
        }

        // Returns zero or more Product records containing the supplied partial Product Name string.
        public List<Product> FindProductsByPartialProductName(string partialProductName)
        {
            Console.WriteLine($"WestWindServices: FindProductsByPartialProductName(); partialProductName= {partialProductName}");
            return _context.Products.Where(x=>x.ProductName.Contains(partialProductName)).ToList();
        }

        // Returns zero or more Product records matching the supplied CategoryId
        public List<Product> FindProductsByCategory(int? selectedCategoryId)
        {
            Console.WriteLine($"WestWindServices: FindProductsByCategory(); selectedCategoryId= {selectedCategoryId}");
            return _context.Products.Where(x=>x.CategoryId == selectedCategoryId).ToList();
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