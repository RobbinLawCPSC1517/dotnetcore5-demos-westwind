//https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/exceptions/exception-handling
using System;

namespace bb_console2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main method started");
            var app = new Program();
            var temp = app.Ex01();
            Console.WriteLine($"returned string: {temp}");
            Console.WriteLine("Main method ended");
        }

        private string Ex01()
        {
            try
            {
                Console.WriteLine("Ex01 try started");
                throw new Exception($"Hey exception was thrown"); 
                Console.WriteLine("Ex01 try ended");
                return "try";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in Ex01: {ex.Message}");
                //throw;
                return "catch";
            }
            finally
            {
                Console.WriteLine("The finally runs exception or not");
                //cannot have a return in finally
                //return "finally";
            }
            
        }
    }
}
