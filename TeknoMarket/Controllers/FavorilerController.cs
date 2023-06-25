using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeknoMarket.Models;

namespace TeknoMarket.Controllers
{
    public class FavorilerController : Controller
    {
        // GET: Favoriler

        [HttpGet]
        public ActionResult Index()
        {

            MySqlConnection cnn;
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarket;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;

            List<Product> productList = new List<Product>();
            List<Product> favoriteList = new List<Product>();

            cnn.Open();

            cmd.CommandText = "SELECT teknomarket.product.id,teknomarket.product.name,teknomarket.product.stock,teknomarket.product.price,teknomarket.product.description,teknomarket.product.image, teknomarket.product.category_id, teknomarket.category.category_name\r\nFROM teknomarket.product\r\nJOIN teknomarket.category ON teknomarket.product.category_id = teknomarket.category.id;\r\n";
            MySqlDataReader drr = cmd.ExecuteReader();

            while (drr.Read())
            {
                Product temp = new Product();
                temp.Id = drr.GetInt32("id");
                temp.name = drr.GetString("name");
                temp.stock = drr.GetInt32("stock");
                temp.price = drr.GetInt32("price");
                temp.description = drr.GetString("description");
                byte[] imageBytes = (byte[])drr["image"];
                temp.imageUrl = "data:image;base64," + Convert.ToBase64String(imageBytes);
                temp.category_name = drr.GetString("category_name");

                switch (temp.category_name)
                {
                    case "Ekran Kartı":
                        temp.category_insider = "ekran-karti";
                        break;
                    case "Bilgisayar Kasası":
                        temp.category_insider = "bilgisayar-kasasi";
                        break;
                    case "Mouse Pad":
                        temp.category_insider = "mouse-pad";
                        break;
                    case "Oyuncu Koltukları":
                        temp.category_insider = "oyuncu-koltuklari";
                        break;
                    case "Şarj Aleti":
                        temp.category_insider = "sarj-aleti";
                        break;
                    case "İşlemci":
                        temp.category_insider = "işlemci";
                        break;
                    default:
                        temp.category_insider = temp.category_name;
                        break;

                }
                productList.Add(temp);
            }
            drr.Close();
            cnn.Close();





            cnn.Open();

            string query = "SELECT product.*, category.category_name\r\nFROM teknomarket.product\r\nINNER JOIN teknomarket.favorite f ON product.id = f.product_item_id\r\nINNER JOIN teknomarket.category ON product.category_id = category.id;\r\n";
            cmd.CommandText = query;
            drr = cmd.ExecuteReader();

            while (drr.Read())
            {
                Product temp = new Product();
                temp.Id = drr.GetInt32("id");
                temp.name = drr.GetString("name");
                temp.stock = drr.GetInt32("stock");
                temp.price = drr.GetInt32("price");
                temp.description = drr.GetString("description");
                byte[] imageBytes = (byte[])drr["image"];
                temp.imageUrl = "data:image;base64," + Convert.ToBase64String(imageBytes);
                temp.category_name = drr.GetString("category_name");

                switch (temp.category_name)
                {
                    case "Ekran Kartı":
                        temp.category_insider = "ekran-karti";
                        break;
                    case "Bilgisayar Kasası":
                        temp.category_insider = "bilgisayar-kasasi";
                        break;
                    case "Mouse Pad":
                        temp.category_insider = "mouse-pad";
                        break;
                    case "Oyuncu Koltukları":
                        temp.category_insider = "oyuncu-koltuklari";
                        break;
                    case "Şarj Aleti":
                        temp.category_insider = "sarj-aleti";
                        break;
                    case "İşlemci":
                        temp.category_insider = "işlemci";
                        break;
                    default:
                        temp.category_insider = temp.category_name;
                        break;

                }
                favoriteList.Add(temp);
            }

            cnn.Close();

            ViewBag.getList = productList;
            ViewBag.getFavoriteList = favoriteList;


            return View();
        }

        
        public ActionResult favoriInner ()
        {



            return PartialView();
        }

        public ActionResult favoriOneriPartial ()
        {

            return PartialView();
        }

        [HttpPost]
        public void favoriSil(string value)
        {


            MySqlConnection cnn;
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarket;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;
            cnn.Open();


            cmd.CommandText = "DELETE FROM favorite WHERE product_item_id = @id";
            cmd.Parameters.AddWithValue("@id", value);

            cmd.ExecuteNonQuery();

            cnn.Close();





        }

    }
}