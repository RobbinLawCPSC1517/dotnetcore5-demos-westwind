using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestWind.Entities;
using WestWind.Services;

namespace MyApp.Namespace
{
    public class CrudModel : PageModel
    {
        private readonly WestWindServices Services;
        public CrudModel(WestWindServices services)
        {
            Services = services;
        }

        public string PartialProductName { get; set; }
        public int? SelectedCategoryId {get;set;}
        public string ButtonPressed { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public Product Product {get;set;} = new();
        public List<Category> SelectListOfCatagories {get;set;}
        public List<Supplier> SelectListOfSuppliers {get;set;}

        public void OnGet()
        {
            try
            {
                Console.WriteLine($"CrudModel: OnGet");
                PopulateSelectLists();
            }
            catch (Exception ex)
            {
                GetInnerException(ex);
            } 
        }

        public IActionResult OnPost(string buttonPressed, string partialProductName, string selectedCategoryId,
            string productId, string productName, string supplierId, string categoryId, string quantityPerUnit,
            string minimumOrderQuantity, string unitPrice, string unitsOnOrder, string discontinued)
        {
            try
            {
                Console.WriteLine($"CrudModel: OnPost");
                ButtonPressed = buttonPressed;
                PartialProductName = partialProductName;
                if(!string.IsNullOrEmpty(selectedCategoryId))
                    SelectedCategoryId = int.Parse(selectedCategoryId);
                if(!string.IsNullOrEmpty(productId))
                    Product.ProductId = int.Parse(productId);
                Product.ProductName = productName;
                if(!string.IsNullOrEmpty(supplierId))
                    Product.SupplierId = int.Parse(supplierId);
                if(!string.IsNullOrEmpty(categoryId))
                    Product.CategoryId = int.Parse(categoryId);
                Product.QuantityPerUnit = quantityPerUnit;
                if(!string.IsNullOrEmpty(minimumOrderQuantity))
                    Product.MinimumOrderQuantity = short.Parse(minimumOrderQuantity);
                if(!string.IsNullOrEmpty(unitPrice))
                    Product.UnitPrice = decimal.Parse(unitPrice);
                if(!string.IsNullOrEmpty(unitsOnOrder))
                    Product.UnitsOnOrder = int.Parse(unitsOnOrder);
                if(string.IsNullOrEmpty(discontinued))
                    Product.Discontinued = false;
                else
                    Product.Discontinued = true;
                PopulateSelectLists();

                if(ButtonPressed == "Update"){
                    if(string.IsNullOrEmpty(Product.ProductName))
                        throw new ArgumentException("ProductName cannot be empty");
                    if(Product.SupplierId == 0)
                        throw new ArgumentException("SupplierId cannot be 0");
                    if(Product.CategoryId == 0)
                        throw new ArgumentException("CategoryId cannot be 0");
                    if(string.IsNullOrEmpty(quantityPerUnit))
                        throw new ArgumentException("QuantityPerUnit cannot be empty");
                    Services.Edit(Product);
                    SuccessMessage = "Update Successful";
                }
                else if(ButtonPressed == "Add"){
                    if(string.IsNullOrEmpty(Product.ProductName))
                        throw new ArgumentException("ProductName cannot be empty");
                    if(Product.SupplierId == 0)
                        throw new ArgumentException("SupplierId cannot be 0");
                    if(Product.CategoryId == 0)
                        throw new ArgumentException("CategoryId cannot be 0");
                    if(string.IsNullOrEmpty(quantityPerUnit))
                        throw new ArgumentException("QuantityPerUnit cannot be empty");
                    Services.Add(Product);
                    SuccessMessage = "Add Successful";
                }
                else if(ButtonPressed == "CrudDelete"){
                    Services.Delete(Product);
                    Product = new Product();
                    SuccessMessage = "Delete Successful";
                }
                else if(!string.IsNullOrEmpty(productId))
                {
                    Product.ProductId = int.Parse(productId);
                    Product = Services.Retrieve(Product.ProductId);
                    SuccessMessage = "Retrieve Successful";
                }
                return Page();
            }
            catch (Exception ex)
            {
                GetInnerException(ex);
                return Page();
            }
        }

        private void PopulateSelectLists()
        {
            try
            {
                Console.WriteLine("CrudModel: PopulateSelectLists");
                SelectListOfCatagories = Services.ListCategories();
                SelectListOfSuppliers = Services.ListSuppliers();
            }
            catch (Exception ex)
            { 
                GetInnerException(ex);
            }
        }

        public void GetInnerException(Exception ex)
        {
            // Start with the assumption that the given exception is the root of the problem
            Exception rootCause = ex;
            // Loop to "drill-down" to what the original cause of the problem is
            while (rootCause.InnerException != null)
                rootCause = rootCause.InnerException;
            ErrorMessage = rootCause.Message;
        }
    }
}
