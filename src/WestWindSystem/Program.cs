using System;
using System.Collections.Generic;
using System.IO;

namespace WestWindSystem
{
    class Program
    {
        //this main was added in Ex01 and subsequently modified.
        static void Main(string[] args)
        {
            var app = new Program();
            app.Ex01();
            //app.Ex02();
            //app.Ex03();
           // return 0;
        }

        private void Ex01()
        {
            try
            {
                Console.WriteLine("Ex01 Program started");
                Supplier theSupplier = new Supplier("Robbins Foods", "780-111-2222");
                ProductLine theProductLine = new ProductLine(theSupplier);
                theProductLine.AddRailCar(new RollingStock("GATX 220455","Alberta Gov",38800,130200,110000,RailCarType.BOX_CAR,1980,true,"none"));
                theProductLine.AddRailCar(new RollingStock("TXLX 152635","Alberta Gov",39200,125200,105000,RailCarType.COVERED_HOPPER,1980,true,"none"));

                //print out the train components to the console.
                Console.WriteLine(theProductLine.Supplier.ToString());
                foreach (var item in theProductLine.Products)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.WriteLine("Ex01 Program ended");
                Console.WriteLine("");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Exception in Ex01: {ex.Message}");
                //throw;
            }
            
        }

        private void Ex02()
        {
            try
            {
                const string FileName = "Ex02.dat";
                Console.WriteLine("Ex02 Program started");
                //create RailCars file only no Engine.
                List<RollingStock> cars = new List<RollingStock>();
                cars.Add(new RollingStock("GATX 220455","Alberta Gov",38800,130200,110000,RailCarType.BOX_CAR,1980,true,"none"));
                cars.Add(new RollingStock("TXLX 152635","Alberta Gov",39200,125200,105000,RailCarType.COVERED_HOPPER,1980,true,"none"));
                cars.Add(new RollingStock("CASR 658974","Alberta Gov",35700,120700,100900,RailCarType.COAL_CAR,1980,true,"none"));
                cars.Add(new RollingStock("CASR 651234","Alberta Gov",35700,120700,100900,RailCarType.COAL_CAR,1980,true,"none"));
                cars.Add(new RollingStock("CASR 674125","Alberta Gov",35700,120700,100900,RailCarType.COAL_CAR,1980,true,"none"));
                
                List<string> csvlines = new();
                foreach (var item in cars)
                {
                    csvlines.Add(item.ToString());
                }
                //write to a csv file. requires System.IO    
                File.WriteAllLines(FileName, csvlines);
                Console.WriteLine("Ex02 Program ended");
                Console.WriteLine("");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Exception in Ex02: {ex.Message}");
                //throw;
            }
        }

        private void Ex03()
        {
            try
            {
                const string FileName = "Ex02.dat";
                Console.WriteLine("Ex03 Program started");
                //create RailCars file only no Engine.
                List<RollingStock> cars = new List<RollingStock>();
                Supplier theSupplier = new Supplier("Robbins Foods", "780-111-2222");
                ProductLine theProductLine = new ProductLine(theSupplier);

                //read the csv file and each line becomes a new car added to the train.
                string[] fileinput = File.ReadAllLines(FileName);
                cars.Clear();
                RollingStock car = null;
                //each line read from the file is a string that now has to be parsed into different types.
                foreach(var line in fileinput)
                {
                    RollingStock.TryParse(line, out car);
                    theProductLine.AddRailCar(car);
                }

                //print out the train components to the console.
                Console.WriteLine(theProductLine.Supplier.ToString());
                foreach (var item in theProductLine.Products)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.WriteLine("Ex03 Program ended");
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in Ex03: {ex.Message}");
                //throw;
            }
            
        }
    }
}
