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
        private readonly TrainWatchServices _services;
        public CrudModel(TrainWatchServices services)
        {
            _services = services;
        }

        public string PartialRecordingMark { get; set; }
        public int? SelectedRailCarTypeId {get;set;}
        public string ButtonPressed { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public RollingStock RollingStock {get;set;} = new();

        public void OnGet(string partialRecordingMark, string selectedRailCarTypeId,
            string reportingMark, string successMessage)
        {
            try
            {
                Console.WriteLine($"CrudModel: OnGet");
                PartialRecordingMark = partialRecordingMark;
                if(!string.IsNullOrEmpty(selectedRailCarTypeId))
                    SelectedRailCarTypeId = int.Parse(selectedRailCarTypeId);
                if(!string.IsNullOrEmpty(reportingMark))
                {
                    RollingStock = _services.Retrieve(reportingMark);
                    SuccessMessage = successMessage; 
                }
            }
            catch (Exception ex)
            {
                GetInnerException(ex);
            } 
        }

        public IActionResult OnPost(string buttonPressed, string partialRecordingMark, string selectedRailCarTypeId,
            string reportingMark, string owner, string lightWeight, string loadLimit, string capacity, string railCarTypeId,
            string yearBuilt, string inService, string notes)
        {
            try
            {
                Console.WriteLine($"CrudModel: OnPost");
                ButtonPressed = buttonPressed;
                PartialRecordingMark = partialRecordingMark;
                if(!string.IsNullOrEmpty(selectedRailCarTypeId))
                    SelectedRailCarTypeId = int.Parse(selectedRailCarTypeId);
                RollingStock.ReportingMark = reportingMark;
                RollingStock.Owner = owner;
                if(!string.IsNullOrEmpty(lightWeight))
                RollingStock.LightWeight = int.Parse(lightWeight);
                if(!string.IsNullOrEmpty(loadLimit))
                RollingStock.LoadLimit = int.Parse(loadLimit);
                if(!string.IsNullOrEmpty(capacity))
                RollingStock.Capacity = int.Parse(capacity);
                if(!string.IsNullOrEmpty(railCarTypeId))
                    RollingStock.RailCarTypeId = int.Parse(railCarTypeId);
                if(!string.IsNullOrEmpty(yearBuilt))
                    RollingStock.YearBuilt = int.Parse(yearBuilt);
                Console.WriteLine($"Actual form checkbox InService={inService}");
                if(string.IsNullOrEmpty(inService))
                {
                    RollingStock.InService = false;
                }
                else
                {
                    RollingStock.InService = true;
                }
                RollingStock.Notes = notes;
            
                if(ButtonPressed == "Update"){
                    _services.Edit(RollingStock);
                    SuccessMessage = "Update Successful";
                }
                else if(ButtonPressed == "Add"){
                    if(string.IsNullOrEmpty(reportingMark)&&string.IsNullOrEmpty(owner))
                        throw new ArgumentException("Reporting Mark and/or Owner cannot be null or empty");
                    if(string.IsNullOrEmpty(reportingMark))
                        throw new ArgumentException("Reporting Mark cannot be null or empty");
                    if(string.IsNullOrEmpty(owner))
                        throw new ArgumentException(" Owner cannot be null or empty");
                    _services.Add(RollingStock);
                    SuccessMessage = "Add Successful";
                }
                else if(ButtonPressed == "CrudDelete"){
                    _services.Delete(RollingStock);
                    SuccessMessage = "Delete Successful";
                    return RedirectToPagePreserveMethod("Query", "", new { successMessage = SuccessMessage});
                }
                else if(ButtonPressed == "CrudCancel"){
                    return RedirectToPagePreserveMethod("Query");
                }
                else if(!string.IsNullOrEmpty(reportingMark))
                {
                    RollingStock = _services.Retrieve(reportingMark);
                    SuccessMessage = "Retrieve Successful";
                }
                return Page();
                //return RedirectToPage();
                //return RedirectToPagePreserveMethod();
            }
            catch (Exception ex)
            {
                GetInnerException(ex);
                return Page();
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
