using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment5.MySqlConnector;
using Newtonsoft.Json;

namespace Assignment5.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price{get; set;}
        public string url { get; set; }
        public string text { get; set; }
        public List<string> imgs { get; set; }
        public string type { get; set; }
    }
}
