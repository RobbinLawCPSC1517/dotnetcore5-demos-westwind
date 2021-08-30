using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrainWatch.Entities;
using TrainWatch.Services;

namespace MyApp.Namespace
{
    public class AboutModel : PageModel
    {
        //a dependency on the TrainWatchServices class via Constructor Dependency Injection
        private readonly TrainWatchServices _services;
        public AboutModel(TrainWatchServices services) {
           _services = services;
        }

        public DbVersion DatabaseVersion { get; set; }

        public string SuccessMessage {get; set;}
        public string ErrorMessage {get; set;}

        public void OnGet()
        {
            try
            {
                Console.WriteLine($"AboutModel: OnGet");
                DatabaseVersion = _services.GetDbVersion();
                SuccessMessage = $"Database Retrieve Successful";
            }
            catch (Exception ex)
            {
                GetInnerException(ex);
            }
            
        }

        public void GetInnerException(Exception ex)
        {
            Exception rootCause = ex;
            while (rootCause.InnerException != null)
                rootCause = rootCause.InnerException;
            ErrorMessage = rootCause.Message;
        }
    }
}
