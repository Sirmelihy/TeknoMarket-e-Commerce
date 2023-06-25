using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using TeknoMarket.Models;

namespace TeknoMarket.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Anasayfa()
        {
            MySqlConnection cnn;
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarket;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);
            string test = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;


            List<Product> list = new List<Product>();

            try
            {

                cnn.Open();
                test = "Sql Connection Successfull";

                cmd.CommandText = "SELECT teknomarket.product.id,teknomarket.product.name,teknomarket.product.stock,teknomarket.product.price,teknomarket.product.description,teknomarket.product.image, teknomarket.product.category_id, teknomarket.category.category_name\r\nFROM teknomarket.product\r\nJOIN teknomarket.category ON teknomarket.product.category_id = teknomarket.category.id;\r\n";
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
                    temp.category_name = dr.GetString("category_name");

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

                    list.Add(temp);
                }
                cnn.Close();



            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return View(list);
        }

        [HttpPost]
        public void AddBasket(string value)
        {
            MySqlConnection cnn;
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarket;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;
            cnn.Open();

            cmd.CommandText = "SELECT COUNT(*) as row_count FROM teknomarket.shopping_cart;";
            object empty = cmd.ExecuteScalar();
            int emptyer = Convert.ToInt32(empty);
            int lastid = 0;

            if(emptyer == 0)
            {
                lastid = 1;
            }

            else
            {
                cmd.CommandText = "SELECT MAX(id) FROM shopping_cart";
                object maxid = cmd.ExecuteScalar();
                lastid = Convert.ToInt32(maxid) + 1;
            }


            cmd.CommandText = "INSERT INTO shopping_cart (id,user_id,product_item_id) VALUES (@id,@user_id,@product_item_id)";
            cmd.Parameters.AddWithValue("@id", lastid);
            cmd.Parameters.AddWithValue("@user_id", 4);
            cmd.Parameters.AddWithValue("@product_item_id", value);
            cmd.ExecuteNonQuery();

            cnn.Close();

            return ;


        }


        [HttpPost]
        public void AddFavorite(string value) {

            bool checkinside = false;

            MySqlConnection cnn;
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarket;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;
            cnn.Open();

            cmd.CommandText = "select * from favorite";
            MySqlDataReader dr = cmd.ExecuteReader();

            List<int> ints= new List<int>();


            while(dr.Read())
            {
                ints.Add(dr.GetInt32("product_item_id"));
            }

            dr.Close();


            foreach (var x in ints)
            {

                if(Convert.ToInt32(value)==x)
                {
                    checkinside = true;
                }
            } 

            if(checkinside)
            {
                cmd.CommandText = "DELETE FROM teknomarket.favorite WHERE product_item_id = @id";
                cmd.Parameters.AddWithValue("@id", value);
                cmd.ExecuteNonQuery();
            }

            else
            {
                cmd.CommandText = "SELECT COUNT(*) as row_count FROM teknomarket.favorite;";
                object empty = cmd.ExecuteScalar();
                int emptyer = Convert.ToInt32(empty);
                int lastid = 0;

                if (emptyer == 0)
                {
                    lastid = 1;
                }

                else
                {
                    cmd.CommandText = "SELECT MAX(id) FROM favorite";
                    object maxid = cmd.ExecuteScalar();
                    lastid = Convert.ToInt32(maxid) + 1;
                }


                cmd.CommandText = "INSERT INTO favorite (id,user_id,product_item_id) VALUES (@id,@user_id,@product_item_id)";
                cmd.Parameters.AddWithValue("@id", lastid);
                cmd.Parameters.AddWithValue("@user_id", 4);
                cmd.Parameters.AddWithValue("@product_item_id", value);
                cmd.ExecuteNonQuery();


            }


            checkinside = false;

            cnn.Close();



        }


    }


    
}
