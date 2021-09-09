using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestWind.Services;
using WestWind.Entities;

namespace MyApp.Namespace
{
    public class QueryModel : PageModel
    {
        private readonly WestWindServices _services;
        public QueryModel(WestWindServices services)
        {
            _services = services;
        }
        public string ButtonPressed {get; set;}
        public string MyButtonPressed {get; set;}
        
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public string PartialProductName {get;set;}
        public int? SelectedCategoryId {get;set;}

        public List<Product> SearchedProducts { get; set; }
        public List<Category> SelectListOfCatagories {get;set;}
        
        public void OnGet(string partialProductName, string selectedCategoryId,
            string productName, string successMessage)
        {
            try
            {
                Console.WriteLine("Query: OnGet");
                PartialProductName = partialProductName;
                if(!string.IsNullOrEmpty(selectedCategoryId))
                    SelectedCategoryId = int.Parse(selectedCategoryId);
                SuccessMessage = successMessage;
                PopulateSelectLists();
            }
            catch (Exception ex)
            {
                GetInnerException(ex);
            }
        }

        public void OnPost(string buttonPressed, string partialProductName, string selectedCategoryId,
            string productName, string successMessage)
        {
            try
            {
                Console.WriteLine("Query: OnPost");
                ButtonPressed = buttonPressed;
                PartialProductName = partialProductName;
                if(!string.IsNullOrEmpty(selectedCategoryId))
                    SelectedCategoryId = int.Parse(selectedCategoryId);
                SuccessMessage = successMessage;
                if(ButtonPressed == "SearchByPartialProductName")
                {
                    SearchedProducts = _services.FindProductsByPartialProductName(PartialProductName);
                }
                else if(ButtonPressed == "SearchByCategory")
                {
                    SearchedProducts = _services.FindProductsByCategory(SelectedCategoryId);
                }
                PopulateSelectLists();
            }
            catch (Exception ex)
            {
                GetInnerException(ex);
            }
            
        }

        private void PopulateSelectLists()
        {
            try
            {
                Console.WriteLine("Query: PopulateSelectLists");
                SelectListOfCatagories = _services.ListCategories();
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
