using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeknoMarket.Models;

namespace TeknoMarket.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(string category)
        {

            if(category == null)
            {

                return RedirectToAction("Anasayfa", "Home");
            }

            string test = "";

            MySqlConnection cnn;
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarket;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;


            List<Product> list = new List<Product>();

            if (category == "ekran-karti")
            {

                cmd.CommandText = "select * from product WHERE category_id=1";

            }

            switch(category)
            {

                case "ekran-karti":
                    cmd.CommandText = "select * from product WHERE category_id=1";
                    break;

                case "işlemci":
                    cmd.CommandText = "select * from product WHERE category_id=2";
                    break;

                case "anakart":
                    cmd.CommandText = "select * from product WHERE category_id=3";
                    break;

                case "ram":
                    cmd.CommandText = "select * from product WHERE category_id=4";
                    break;

                case "bilgisayar-kasasi":
                    cmd.CommandText = "select * from product WHERE category_id=5";
                    break;
                
                case "Soğutucu":
                    cmd.CommandText = "select * from product WHERE category_id=6";
                    break;

                case "Monitör":
                    cmd.CommandText = "select * from product WHERE category_id=7";
                    break;

                case "Klavye":
                    cmd.CommandText = "select * from product WHERE category_id=8";
                    break;

                case "Mouse":
                    cmd.CommandText = "select * from product WHERE category_id=9";
                    break;

                case "Kulaklık":
                    cmd.CommandText = "select * from product WHERE category_id=10";
                    break;

                case "mouse-pad":
                    cmd.CommandText = "select * from product WHERE category_id=11";
                    break;
                case "mikrofon":
                    cmd.CommandText = "select * from product WHERE category_id=12";
                    break;
                case "kamera":
                    cmd.CommandText = "select * from product WHERE category_id=13";
                    break;
                case "oyuncu-koltuklari":
                    cmd.CommandText = "select * from product WHERE category_id=14";
                    break;
                case "kontrolcu":
                    cmd.CommandText = "select * from product WHERE category_id=15";
                    break;
                case "konsol":
                    cmd.CommandText = "select * from product WHERE category_id=16";
                    break;
                case "sarj-aleti":
                    cmd.CommandText = "select * from product WHERE category_id=18";
                    break;
                case "canta":
                    cmd.CommandText = "select * from product WHERE category_id=19";
                    break;
                case "kablo":
                    cmd.CommandText = "select * from product WHERE category_id=20";
                    break;

                default:
                    cmd.CommandText = "select * from product WHERE category_id=1";
                    break;


            }


            try
                {

                    cnn.Open();
                    test = "Sql Connection Successfull";
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

                catch(Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }






            return View(list);
        }
    }
}