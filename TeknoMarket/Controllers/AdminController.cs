using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using TeknoMarket.Models;
using System.Web.Security;
using System.IO;

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
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarket;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;


            List<Product> list = new List<Product>();
            


            try
            {
                cnn.Open();


                test = "SQL CONNECTION SUCCESSFULL";
                ViewBag.test = test;


                cmd.CommandText = "select * from product";
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Product temp = new Product();
                    temp.Id = dr.GetInt32("id");
                    temp.name = dr.GetString("name");
                    temp.stock = dr.GetInt32("stock");
                    temp.price = dr.GetInt32("price");
                    temp.description = dr.GetString("description");
                    byte[] imageBytes = (byte[])dr["image"];
                    temp.imageUrl = "data:image;base64," + Convert.ToBase64String(imageBytes);

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
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(string category_id,string product_name,string product_stock,string product_price,string product_description,HttpPostedFileBase filebutton)
        {
            MySqlConnection cnn;
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarket;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;

            byte[] imgdata = null;
            
            string test = "";
            


            try
            {
                cnn.Open();


                using (var binaryReader = new BinaryReader(filebutton.InputStream))
                {
                    imgdata = binaryReader.ReadBytes(filebutton.ContentLength);
                }


                test = "SQL CONNECTION SUCCESSFULL";

                cmd.CommandText = "SELECT MAX(id) FROM product";
                object maxid = cmd.ExecuteScalar();
                int lastid = Convert.ToInt32(maxid) + 1;

                cmd.CommandText = "INSERT INTO product (id,category_id,name,stock,price,description,image) VALUES (@id,@catid,@name,@stock,@price,@description,@img)";
                cmd.Parameters.AddWithValue("@id", lastid);
                cmd.Parameters.AddWithValue("@catid", Convert.ToInt32(category_id));
                cmd.Parameters.AddWithValue("@name", product_name);
                cmd.Parameters.AddWithValue("@stock", Convert.ToInt32(product_stock));
                cmd.Parameters.AddWithValue("@price", Convert.ToInt32(product_price));
                cmd.Parameters.AddWithValue("@description", product_description);
                cmd.Parameters.AddWithValue("@img", imgdata);
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                test = "SQL CONNECTION NOT SUCCESSFULL";
                ViewBag.test = test;
            }
            return View ();
        }

        public ActionResult listAccounts ()
        {

            string test = "";

            MySqlConnection cnn;
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarket;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;


            List<Account> list = new List<Account>();



            try
            {
                cnn.Open();


                test = "SQL CONNECTION SUCCESSFULL";
                ViewBag.test = test;


                cmd.CommandText = "select * from user";
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Account temp = new Account();
                    temp.Id = dr.GetInt32("id");
                    temp.email = dr.GetString("email");
                    temp.password = dr.GetString("password");
                    temp.name = dr.GetString("name");
                    temp.surname = dr.GetString("surname");
                    temp.phone = dr.GetString("phone");
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





        public ActionResult AddCategory ()
        {

            return View();
        }


        [HttpPost]
        public ActionResult AddCategory (string category_name)
        {

            string test = "";

            MySqlConnection cnn;
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarket;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;


            cnn.Open();
            cmd.CommandText = "SELECT MAX(id) from category";
            object id = cmd.ExecuteScalar();

            int lastid = Convert.ToInt32(id);

            cmd.CommandText = "INSERT INTO category (id,category_name) VALUES (" + (lastid+1) + ",'" + category_name + "')";
            cmd.ExecuteNonQuery();

            test = "SQL CONNECTION SUCCESSFUL";

            cnn.Close();


            ViewBag.test = test;

            return View();
        }



    }
}