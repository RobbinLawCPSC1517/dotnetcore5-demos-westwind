//https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class ContactModel : PageModel
    {
        // Allows for one-way binding of data
        [BindProperty]
        public string Text1{get;set;}
        
        // Allows for two-way binding of data
        [BindProperty] 
        public string Text3{get;set;}

        [BindProperty] 
        public int Number1{get;set;}

        public string Email{get;set;}
        public List<string> SelectListOfSubjects{get;set;}
        public int SelectedSubjectId {get;set;}
        public string MessageBody{get;set;}
        public bool ActiveMember{get;set;}

        public string ButtonPressed {get; set;}
        public string SuccessMessage {get; set;}
        public string ErrorMessage {get; set;}

        public IActionResult OnGet()
        {
            try
            {
                Console.WriteLine($"ContactModel: OnGet");
                PopulateSelectLists();
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

        public IActionResult OnPost(string buttonPressed, string text1, string email, string selectedSubjectId, 
            string messageBody, string activeMember)
        {
            try
            {
                Console.WriteLine($"ContactModel: OnPost");
                ButtonPressed = buttonPressed;
                if(ButtonPressed == "Clear")
                    return RedirectToPage("Contact");
                PopulateSelectLists();
                Text1 = text1;
                Email = email;
                if(!string.IsNullOrEmpty(selectedSubjectId))
                    SelectedSubjectId = int.Parse(selectedSubjectId);
                MessageBody = messageBody;
                Console.WriteLine($"Actual form checkbox={activeMember}");
                if(string.IsNullOrEmpty(activeMember))
                {
                    ActiveMember = false;
                }
                else
                {
                    ActiveMember = true;
                }
                if (string.IsNullOrEmpty(Email) ||
                SelectedSubjectId == 0 ||
                string.IsNullOrEmpty(Text1) ||
                string.IsNullOrEmpty(MessageBody))
                {
                    throw new Exception("All fields are required");
                }
                SuccessMessage = $"T1: {Text1} T3: {Text3} N1: {Number1} Email: {Email} Subject: {SelectListOfSubjects[SelectedSubjectId]} Text: {MessageBody} ActiveMember: {ActiveMember}";
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

        private void PopulateSelectLists()
        {
            try
            {
                SelectListOfSubjects = new List<string>(){"select...", "Contributing", "Request Membership", "Bug Report"};  
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