﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace TeknoMarket.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Anasayfa()
        {
            
            return View();
        }
    }
}
