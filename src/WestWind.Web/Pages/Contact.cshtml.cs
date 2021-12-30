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
        public string Text1{get;set;}
        // Allows for two-way binding of data
        [BindProperty] 
        public string Text2{get;set;}
        // Allows for two-way binding of data
        [BindProperty] 
        public string Text3{get;set;}

        public int Number1{get;set;}
        [BindProperty] 
        public int Number2{get;set;}
        [BindProperty] 
        public int Number3{get;set;}
        [BindProperty]
        public string Email{get;set;}
        public List<string> SelectListOfSubjects{get;set;}
        [BindProperty]
        public int SelectedSubjectId {get;set;}
        [BindProperty]
        public string MessageBody{get;set;}
        [BindProperty]
        public bool ActiveMember{get;set;}
        [BindProperty]
        public string ButtonPressed {get; set;}
        public string SuccessMessage {get; set;}
        public string ErrorMessage {get; set;}
        public List<Exception> errors {get; set;} = new();

        public IActionResult OnGet()
        {
            try
            {
                Console.WriteLine($"ContactModel: OnGet");
                PopulateSelectLists();
                // Return the page but preserve any user inputs
                return Page();
            }
            catch (AggregateException ex)
            {
                GetInnerException(ex);
                // Return the page but preserve any user inputs
                return Page();
            }
        }

        public IActionResult OnPost(string buttonPressed, string text1, string number1, string email, string selectedSubjectId, string messageBody, string activeMember)
        {
            try
            {
                Console.WriteLine($"ContactModel: OnPost");
                //ButtonPressed = buttonPressed;
                if(ButtonPressed == "Submit")
                {
                    PopulateSelectLists();
                    Text1 = text1;
                    //Email = email;
                    // if(!string.IsNullOrEmpty(selectedSubjectId))
                    //     SelectedSubjectId = int.Parse(selectedSubjectId);
                    // MessageBody = messageBody;
                    Console.WriteLine($"Actual form checkbox={activeMember}");
                    if(string.IsNullOrEmpty(activeMember))
                    {
                        ActiveMember = false;
                    }
                    else
                    {
                        ActiveMember = true;
                    }
                    if (SelectedSubjectId == 0)
                        errors.Add(new Exception("Subject"));
                    if (string.IsNullOrEmpty(Text1))
                        errors.Add(new Exception("Text1"));
                    if (errors.Count() > 0)
                        throw new AggregateException("Missing Data: ", errors);
                    SuccessMessage = $"T1: {Text1} T2: {Text2} T3: {Text3} N1: {Number1} N2: {Number2} Email: {Email} Subject: {SelectListOfSubjects[SelectedSubjectId]} Text: {MessageBody} ActiveMember: {ActiveMember}";
                } else if(ButtonPressed == "Clear")
                {

                }
                // Return the page but preserve any user inputs
                return Page();
            }
            catch (AggregateException e)
            {
                
                GetInnerException(e);
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
            catch (AggregateException ex)
            { 
                GetInnerException(ex);
            }
        }

        public void GetInnerException(AggregateException e)
        {
            ErrorMessage = e.Message;
            foreach (Exception innerException in e.InnerExceptions)
                {
                    //ErrorMessage += innerException.Message;
                    Console.WriteLine(innerException.Message);
                }
            // Start with the assumption that the given exception is the root of the problem
            //Exception rootCause = ex;
            // Loop to "drill-down" to what the original cause of the problem is
            // while (rootCause.InnerException != null)
            //     rootCause = rootCause.InnerException;
            //ErrorMessage = rootCause.Message;
        }
    }
}