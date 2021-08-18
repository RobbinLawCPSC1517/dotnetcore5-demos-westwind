using System;

namespace WestWindSystem
{
    //the following class was added in Ex01.
    public class RollingStock
    {
        //the following 8 fields and 2 properties were added in Ex01.
        public readonly string ReportingMark;
        public readonly string Owner;
        public readonly int LightWeight;
        public readonly int LoadLimit;
        public readonly int Capacity;
        public readonly RailCarType Type;
        public readonly int YearBuilt;
        public readonly bool InService;
        public readonly string Notes;
        public int GrossWeight { get; private set; }
        public int NetWeight { get { return GrossWeight - LightWeight; } }
        //using the lambda operator.
        //public int NetWeight => GrossWeight - LightWeight;

        //the following const was added in Ex02.
        private const string SPECIALCHARACTERS =@",:;\/!?@#$%^&*~`";

        //the following constructor was added in Ex01 and modified in Ex02.
        public RollingStock(string reportingMark, string owner, int lightWeight, 
            int loadLimit, int capacity,  RailCarType type, int yearBuilt, bool inService, string notes)
        {
            if (string.IsNullOrEmpty(reportingMark)) 
                throw new ArgumentNullException("Reporting Mark cannot be null or empty");
            foreach(var character in SPECIALCHARACTERS)
                if (reportingMark.Contains(character))
                    throw new FormatException($"Reporting Mark contains an invalid character.");
            if (lightWeight % 100 != 0) 
                throw new ArgumentException("Invalid light weight; weights must be in 100 lb increments");
            if (loadLimit % 100 != 0) 
                throw new ArgumentException("Invalid load limit weight; weights must be in 100 lb increments");
            if (capacity % 100 != 0) 
                throw new ArgumentException("Invalid capacity weight; weights must be in 100 lb increments");
            if (capacity >= loadLimit) 
                throw new ArgumentException("Load Limit must be greater than Capacity");
            
            ReportingMark = reportingMark.Trim();
            Owner = owner;
            LightWeight = lightWeight;
            LoadLimit = loadLimit;
            Capacity = capacity;
            Type = type;
            YearBuilt = yearBuilt;
            InService = inService;
            Notes = notes;
            //When we create a rail car its initial GrossWeight must be its lightWeight, but it is empty of freight at this time.
            GrossWeight = LightWeight;
        }

        //the following method was added in Ex01.
        public void RecordScaleWeight(int grossWeight)
        {
            if (grossWeight % 100 != 0) 
                throw new ArgumentException("Invalid gross weight; weights must be in 100 lb increments");
            if (grossWeight < LightWeight) 
                throw new ArgumentException($"Scale Error - gross weight cannot be less than the light weight of the railcar - {LightWeight} lbs.");
            if (grossWeight > LightWeight + LoadLimit) 
                throw new ArgumentException($"Unsafe Load - exceeding the load limit of {LoadLimit} lbs.");

            GrossWeight = grossWeight;
        }

        //the following method was added in Ex02.
        public override string ToString()
       {
           return $"{ReportingMark},{Owner},{LightWeight},{LoadLimit},{Capacity},{Type},{YearBuilt},{InService},{Notes}";
       }

        //the following method was added in Ex02.
       public static RollingStock Parse(string text)
       {
            string [] items = text.Split(',');
            if (items.Length != 9) 
                throw new FormatException("Input string is not the correct CSV format" );
            return new RollingStock(
                items[0],
                items[1],
                int.Parse(items[2]),
                int.Parse(items[3]),
                int.Parse(items[4]),
                (RailCarType)Enum.Parse(typeof(RailCarType), items[5]),
                int.Parse(items[6]),
                bool.Parse(items[7]),
                items[8]
            );
       }
        //items[5].DehumanizeTo<RailCarType>(),
        //the following method was added in Ex02.
       public static bool TryParse(string text, out RollingStock result)
       {
           bool valid = false;
            try
            {
                result = Parse(text);
                valid = true;
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"catch RollingStock.TryParse: {ex.Message}");  
                result = null;
            }
            return valid;
       }
    }
}