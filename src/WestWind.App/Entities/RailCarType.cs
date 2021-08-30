using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace TrainWatch.Entities {
    public class RailCarType {
        public int RailCarTypeId { get; set; }
        public string Name { get; set; }
        public string Commodity { get; set; }
    }
}