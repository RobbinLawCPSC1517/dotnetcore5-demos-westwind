using System;
using System.Collections.Generic;

namespace WestWindSystem
{
    public class ProductLine
    {
        //the following 5 properties were added in Ex01.
        public Supplier Supplier { get; private set; }
        public List<RollingStock> Products { get; private set; } = new();
        //TotalCars does not include the engine.
        public int TotalProducts { get { return Products.Count; } }
        //using lambda syntax.
        //public int TotalProducts => Products.Count;
        
        //the following constructor was added in Ex01.
        public ProductLine(Supplier givenSupplier)
        {
            Supplier = givenSupplier;
            //RailCars = new List<RollingStock>();
            //RailCars = new ();
        }

        //the following method was added in Ex01 and modified in Ex02.
        public void AddRailCar(RollingStock car)
        {
            //the following if was added in Ex01.
            if (car == null)
                throw new ArgumentNullException(string.Empty,"No car use supplied. Car not added.");
            bool found = false;
            //the following foreach and if were added in Ex02.
            foreach(var existingcar in Products)
                if (car.ReportingMark.Equals(existingcar.ReportingMark))
                    found = true;
            //the following if was added in Ex02.
            if (found)
                throw new ArgumentException($"The railcar {car.ReportingMark} is already attached to the train.");
            Products.Add(car); 
        }
    }
}