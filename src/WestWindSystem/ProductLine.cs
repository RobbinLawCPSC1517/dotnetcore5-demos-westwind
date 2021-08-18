using System;
using System.Collections.Generic;

namespace WestWindSystem
{
    public class ProductLine
    {
        //the following 5 properties were added in Ex01.
        public Supplier Supplier { get; private set; }
        public List<Product> Products { get; private set; } = new();
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
        public void AddProduct(Product product)
        {
            //the following if was added in Ex01.
            if (product == null)
                throw new ArgumentNullException(string.Empty,"No product supplied. Product added.");
            bool found = false;
            //the following foreach and if were added in Ex02.
            foreach(var existingProduct in Products)
                if (product.ProductName.Equals(existingProduct.ProductName))
                    found = true;
            //the following if was added in Ex02.
            if (found)
                throw new ArgumentException($"The railcar {product.ProductName} is already attached to the train.");
            Products.Add(product); 
        }
    }
}