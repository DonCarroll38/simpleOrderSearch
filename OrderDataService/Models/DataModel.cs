using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderDataService.Models
{
    public class DataModel
    {
        public long OrderID { get; set; }
        public int ShipperID { get; set; }
        public int DriverID { get; set; }
        public DateTime CompletionDte{ get; set; }
        public int Status { get; set; }
        public string Code { get; set; }
        public int MSA { get; set; }
        public double Duration { get; set; }
        public int OfferType { get; set; }
    }
}