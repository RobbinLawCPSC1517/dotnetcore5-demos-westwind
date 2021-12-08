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
        private readonly WestWindServices Services;
        public QueryModel(WestWindServices services)
        {
            Services = services;
        }
        public string ButtonPressed {get; set;}
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public string PartialProductName {get;set;}
        public int? SelectedCategoryId {get;set;}

        public List<Product> SearchedProducts { get; set; }
        public List<Category> SelectListOfCatagories {get;set;}
        
        public IActionResult OnGet()
        {
            try
            {
                Console.WriteLine("QueryModel: OnGet");
                PopulateSelectList();
                // Return the page but preserve any user inputs
                return Page();
            }
            catch (Exception ex)
            {
                GetInnerException(ex);
                // Return the page but preserve any user inputs
                return Page();
            }
        }

        public IActionResult OnPost(string buttonPressed, string partialProductName, string selectedCategoryId, string successMessage)
        {
            try
            {
                Console.WriteLine("QueryModel: OnPost");
                ButtonPressed = buttonPressed;
                PartialProductName = partialProductName;
                if(!string.IsNullOrEmpty(selectedCategoryId))
                    SelectedCategoryId = int.Parse(selectedCategoryId);
                SuccessMessage = successMessage;
                if(ButtonPressed == "SearchByPartialProductName")
                {
                    SearchedProducts = Services.FindProductsByPartialProductName(PartialProductName);
                }
                else if(ButtonPressed == "SearchByCategory")
                {
                    SearchedProducts = Services.FindProductsByCategory(SelectedCategoryId);
                }
                PopulateSelectList();
                // Return the page but preserve any user inputs
                return Page();
            }
            catch (Exception ex)
            {
                GetInnerException(ex);
                // Return the page but preserve any user inputs
                return Page();
            }
            
        }

        private void PopulateSelectList()
        {
            try
            {
                Console.WriteLine("Querymodel: PopulateSelectList");
                SelectListOfCatagories = Services.ListCategories();
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
