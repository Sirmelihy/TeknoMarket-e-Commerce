using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeknoMarket.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }

        public string surname { get; set; }
        public string phone { get; set; }
        public string type { get; set; }




    }
}