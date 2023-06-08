using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using TeknoMarket.Models;

namespace TeknoMarket.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult AdminHome ()
        {
            return View();
        }

        public ActionResult testList()
        {
            string test = "";

            MySqlConnection cnn;
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarkettest;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;


            List<testModel> list = new List<testModel>();
            


            try
            {
                cnn.Open();


                test = "SQL CONNECTION SUCCESSFULL";
                ViewBag.test = test;


                cmd.CommandText = "select * from products";
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    testModel temp = new testModel();
                    temp.productid = dr.GetInt32("idProduct");
                    temp.productname = dr.GetString("nameProduct");
                    list.Add(temp);
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                test = "SQL CONNECTION NOT SUCCESSFULL";
                ViewBag.test = test;
            }



            return View(list);
        }
        [Authorize]
        // GET: Admin
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(string product_id,string product_name)
        {
            MySqlConnection cnn;
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarkettest;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;

            string test = "";

            try
            {
                cnn.Open();


                test = "SQL CONNECTION SUCCESSFULL";
                ViewBag.test = test;


                cmd.CommandText = "INSERT INTO products (idProduct,nameProduct) VALUES ("+product_id+",'"+product_name+"')";
                cmd.ExecuteNonQuery();

                cnn.Close();
            }
            catch (Exception ex)
            {
                test = "SQL CONNECTION NOT SUCCESSFULL";
                ViewBag.test = test;
            }


            var p = (string)Session["password"];
            ViewBag.p = p;
            return View ();
        }

        public ActionResult listAccounts ()
        {

            string test = "";

            MySqlConnection cnn;
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarkettest;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;


            List<Account> list = new List<Account>();



            try
            {
                cnn.Open();


                test = "SQL CONNECTION SUCCESSFULL";
                ViewBag.test = test;


                cmd.CommandText = "select * from accounts";
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Account temp = new Account();
                    temp.Id = dr.GetInt32("id");
                    temp.username = dr.GetString("username");
                    temp.password = dr.GetString("password");
                    temp.type = dr.GetString("type");

                    list.Add(temp);
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                test = "SQL CONNECTION NOT SUCCESSFULL";
                ViewBag.test = test;
            }


            return View(list);
        }

        public ActionResult tester()
        {
            return View();

        }
    }
}