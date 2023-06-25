using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeknoMarket.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string name { get; set; }
        public int stock { get; set; }
        public int price { get; set; }
        public string description { get; set; }

        public string imageUrl { get; set; }

        public string category_name { get; set; }

        public string category_insider { get; set;}


    }
}