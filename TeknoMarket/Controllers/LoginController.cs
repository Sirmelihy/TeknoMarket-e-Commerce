using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeknoMarket.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            // Veritabanı bağlantısı
            string connectionString = ConfigurationManager.ConnectionStrings["teknomarketlogin"].ConnectionString;

            // Veritabanı bağlantısını oluşturma
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Veritabanı sorgusu
                string query = "SELECT * FROM users WHERE Email = @Email AND Password = @Password";

                // Veritabanı komutu 
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Parametreler
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    // Veritabanı bağlantısını açma
                    connection.Open();

                    // Veritabanından verileri okuma
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // Giriş başarılı, kullanıcıyı anasayfaya yönlendirilecek
                        return RedirectToAction("Anasayfa", "Home");
                    }
                    else
                    {
                        // Giriş başarısız, hata mesajını
                        ViewBag.ErrorMessage = "Geçersiz e-posta veya şifre";
                        return View();
                    }
                }
            }
        }
    }
}