using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeknoMarket.Models;

namespace TeknoMarket.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        public ActionResult Index(string product)
        {
            if (product == null)
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
            List<Product> partialList = new List<Product>();

            cnn.Open();

            string query = "select * from product WHERE id =" + product + " ";
            string queryy = "SELECT p.*, c.category_name\r\nFROM teknomarket.product p\r\nJOIN teknomarket.category c ON p.category_id = c.id\r\nWHERE p.id =" + product + ";\r\n";
            cmd.CommandText = queryy;
            //cmd.Parameters.AddWithValue("@x", product);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                byte[] imageBytes = (byte[])dr["image"];

                list.Add(new Product()
                {
                    Id = dr.GetInt32("id"),
                    name = dr.GetString("name"),
                    stock = dr.GetInt32("stock"),
                    price = dr.GetInt32("price"),
                    description = dr.GetString("description"),
                    imageUrl = "data:image;base64," + Convert.ToBase64String(imageBytes),
                    category_name = dr.GetString("category_name")
            });

            }
            dr.Close();

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
                partialList.Add(temp);
            }

            cnn.Close();

            ViewBag.getList = partialList;

            return View(list);
        }



        public ActionResult cokSatanlar ()
        {
            MySqlConnection cnn;
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarket;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;

            List<Product> list = new List<Product>();


            cnn.Open();
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



            return PartialView("cokSatanlar", list);
        }



    }
}