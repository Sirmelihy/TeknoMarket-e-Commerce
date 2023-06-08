using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            MySqlConnection cnn;
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarkettest;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);
            string test = "";

            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand();
                test = "SQL CONNECTION SUCCESSFULl";
                ViewBag.test = test;
                cnn.Close();
            }
            catch (Exception ex)
            {
                test = "SQL CONNECTION NOT SUCCESSFULL";
                ViewBag.test = test;
            }
            return View();
        }
    }
}
