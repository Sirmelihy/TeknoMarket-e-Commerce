using System;
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
            MySqlConnection cnn;
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarkettest;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;

            

            string test = "";
            string testt = "";

            try
            {
                cnn.Open();         
                cmd.CommandText = "select surname from testtable WHERE name='zeynep' "; 
                object deneme = cmd.ExecuteScalar();    
                test = "SQL CONNECTION SUCCESSFULl";
                testt = "zeynep soyadı" + deneme.ToString();


                ViewBag.test = test;
                ViewBag.testt = testt;

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