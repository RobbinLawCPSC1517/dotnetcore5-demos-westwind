using System;

namespace bb_console2
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new Program();
            app.Ex01();
        }

        private void Ex01()
        {
            try
            {
                Console.WriteLine("Ex01 Program started");
                //throw new Exception($"Hey Man an exception was thrown"); 
                Console.WriteLine("Ex01 Program ended");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Exception in Ex01: {ex.Message}");
                //throw;
            }
            finally
            {
                Console.WriteLine("The finally runs exception or not");
            }
            
        }
    }
}
