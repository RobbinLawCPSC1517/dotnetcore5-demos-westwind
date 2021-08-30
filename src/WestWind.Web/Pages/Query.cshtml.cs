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
        private readonly TrainWatchServices _services;
        public QueryModel(TrainWatchServices services)
        {
            _services = services;
        }
        public string ButtonPressed {get; set;}
        public string MyButtonPressed {get; set;}
        
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public string PartialRecordingMark {get;set;}
        public int? SelectedRailCarTypeId {get;set;}

        public List<RollingStock> SearchedRollingStocks { get; set; }
        public List<RailCarType> SelectListOfRailCarTypes {get;set;}
        
        public string MyTest1(){
            return "hey man";
        }

        public void OnGet(string partialRecordingMark, string selectedRailCarTypeId,
            string reportingMark, string successMessage)
        {
            try
            {
                Console.WriteLine("Query: OnGet");
                PartialRecordingMark = partialRecordingMark;
                if(!string.IsNullOrEmpty(selectedRailCarTypeId))
                    SelectedRailCarTypeId = int.Parse(selectedRailCarTypeId);
                SuccessMessage = successMessage;
                PopulateSelectLists();
            }
            catch (Exception ex)
            {
                GetInnerException(ex);
            }
        }

        public void OnPost(string buttonPressed, string partialRecordingMark, string selectedRailCarTypeId,
            string reportingMark, string successMessage)
        {
            try
            {
                Console.WriteLine("Query: OnPost");
                ButtonPressed = buttonPressed;
                PartialRecordingMark = partialRecordingMark;
                if(!string.IsNullOrEmpty(selectedRailCarTypeId))
                    SelectedRailCarTypeId = int.Parse(selectedRailCarTypeId);
                SuccessMessage = successMessage;
                if(ButtonPressed == "SearchByPartialRecordingMark")
                {
                    SearchedRollingStocks = _services.FindRollingStocksByPartialReportingMark(PartialRecordingMark);
                }
                else if(ButtonPressed == "SearchByRailCarType")
                {
                    SearchedRollingStocks = _services.FindRollingStocksByRailCarType(SelectedRailCarTypeId);
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
                SelectListOfRailCarTypes = _services.ListRailCarTypes();  
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
