using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderDataService.Models
{
    public class DataParams
    {
        public long OrderID { get; set; }
        public int MSA { get; set; }
        public int Status { get; set; }
        public DateTime CompletionDte { get; set; }
    }
}