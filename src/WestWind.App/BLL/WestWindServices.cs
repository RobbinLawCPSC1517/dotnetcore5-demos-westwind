//https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator

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
        private readonly WestWindContext Context;
        public  WestWindServices(WestWindContext context) {
            if (context == null)
                throw new ArgumentNullException();
            Context = context;
            //null-coalescing operator ??
            //Context = context ?? throw new ArgumentNullException();
        }

        #region QUERY
        //added in Ex04
        public BuildVersion GetDbVersion() 
        {
            Console.WriteLine($"WestWindServices: GetDbVersion;");
            var result = Context.BuildVersion.ToList();
            return result.First();
        }
        //added in Ex05
        // Returns all Category records.
        public List<Category> ListCategories()
        {
            Console.WriteLine($"WestWindServices: ListCategories();");
            return Context.Categories.ToList();
        }
        //added in Ex05
        // Returns zero or more Product records matching the supplied CategoryId
        public List<Product> FindProductsByCategory(int? selectedCategoryId)
        {
            Console.WriteLine($"WestWindServices: FindProductsByCategory(); selectedCategoryId= {selectedCategoryId}");
            return Context.Products.Where(x=>x.CategoryId == selectedCategoryId)
                                   .OrderBy(x => x.ProductName).ToList();
        }
        //added in Ex05
        // Returns zero or more Product records containing the supplied partial Product Name string.
        public List<Product> FindProductsByPartialProductName(string partialProductName)
        {
            Console.WriteLine($"WestWindServices: FindProductsByPartialProductName(); partialProductName= {partialProductName}");
            return Context.Products.Where(x=>x.ProductName.Contains(partialProductName))
                                   .OrderBy(x => x.ProductName).ToList();
        }
        //added in Ex05
        // Returns all Supplier records.
        public List<Supplier> ListSuppliers()
        {
            Console.WriteLine($"WestWindServices: ListSuppliers();");
            return Context.Suppliers.ToList();
        }        
        #endregion
        
        #region READ - Retrieve, Edit, Add, Delete
        //added in Ex06
        public Product Retrieve(int productId)
        {
            Console.WriteLine($"WestWindServices: Retrieve; productId = {productId}");
            return Context.Products.Find(productId);
        }
        //added in Ex06
        public void Edit(Product item)
        {
            Console.WriteLine($"WestWindServices: Edit; productId = {item.ProductId}");
            var existing = Context.Entry(item);
            existing.State = EntityState.Modified;
            Context.SaveChanges();
        }
        //added in Ex06
        public void Add(Product item)
        {
            Console.WriteLine($"WestWindServices: Add; productId= {item.ProductId}");
            Context.Products.Add(item);
            Context.SaveChanges();
        }
        //added in Ex06
        public void Delete(Product item)
        {
            Console.WriteLine($"WestWindServices: Delete; productId= {item.ProductId}");
            var existing = Context.Entry(item);
            existing.State = EntityState.Deleted;
            Context.SaveChanges();
        }
        #endregion
    }
}