using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Security;
using TeknoMarket.Models;

namespace TeknoMarket.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        public ActionResult Login()
        {
            string test = "NO HttpPost yet";
            ViewBag.test = test;

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Adres");
            }


            return View();
        }





        [HttpPost]
        public ActionResult Login(string typeEmailX,string typePasswordX)
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
                    temp.password= dr.GetString("password");
                    temp.name = dr.GetString("name");
                    temp.surname = dr.GetString("surname");
                    temp.phone = dr.GetString("phone");
                    temp.type = dr.GetString("type");

                    list.Add(temp);
                }

                foreach (var x in list)
                {
                    if (typeEmailX == x.email && typePasswordX == x.password)
                    {
                        if(x.type == "admin")
                        {
                            FormsAuthentication.SetAuthCookie(x.email, false);
                            Session["email"] = x.email;
                            Session["password"] = x.password;
                            Session["name"] = x.name;
                            Session["surname"] = x.surname;
                            Session["phone"] = x.phone;
                            Session["type"] = x.type;
                            return RedirectToAction("AddProduct", "Admin");
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(x.email, false);
                            Session["email"] = x.email;
                            Session["password"] = x.password;
                            Session["name"] = x.name;
                            Session["surname"] = x.surname;
                            Session["phone"] = x.phone;
                            Session["type"] = x.type;
                            return RedirectToAction("Anasayfa", "Home");
                        }
                        

                        
                    }
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










        public ActionResult Register()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Anasayfa", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Register(string typeisimX,string typeSoyisimX,string typeEmailX,string typePasswordX,string typePasswordagainX)
        {

            string test = "";

            MySqlConnection cnn;
            string connectionstring = "Server=localhost;Port=3307;Database=teknomarket;Uid=root;Pwd=;";
            cnn = new MySqlConnection(connectionstring);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnn;

            string sendHata = "KAYIT Başarılı";

            try
            {
                cnn.Open();
                test = "SQL CONNECTION SUCCESSFULL";
                ViewBag.test = test;

                

                cmd.CommandText = "select * from user";
                MySqlDataReader dr = cmd.ExecuteReader();
                string tempUsername;
                var checkList = new List<string>();
                while (dr.Read())
                {
                    tempUsername = dr.GetString("email");

                    checkList.Add(tempUsername);
                }
                dr.Close();

                bool checkUsername = false;

                foreach(var x in checkList)
                {
                    if(typeEmailX == x)
                    {
                        checkUsername = false;
                    }
                    else
                    {
                        checkUsername = true;
                    }
                }
                

                if (typePasswordX == typePasswordagainX && checkUsername )
                {
                    string lastidText;
                    cmd.CommandText = "SELECT MAX(id) FROM accounts";
                    object idGetter = cmd.ExecuteScalar();
                    int lastid = Convert.ToInt32(idGetter);
                    lastidText = (lastid + 1).ToString();
                    cmd.CommandText = "INSERT INTO accounts (id,email,password,name,surname,phone,type) VALUES (@id,@email,@password,@name,@surname,@phone,@type)";
                    cmd.Parameters.AddWithValue("@id", lastidText);
                    cmd.Parameters.AddWithValue("@email", typeEmailX);
                    cmd.Parameters.AddWithValue("@password", typePasswordX);
                    cmd.Parameters.AddWithValue("@name", typeisimX);
                    cmd.Parameters.AddWithValue("@surname", typeSoyisimX);
                    cmd.Parameters.AddWithValue("@phone", " ");
                    cmd.Parameters.AddWithValue("@type", "user");
                    cmd.ExecuteNonQuery();

                    FormsAuthentication.SetAuthCookie(typeEmailX, false);
                    Session["email"] = typeEmailX;
                    Session["password"] = typePasswordX;
                    Session["name"] = typeisimX;
                    Session["surname"] = typeSoyisimX;
                    Session["phone"] = " ";
                    Session["type"] = "user";
                    return RedirectToAction("Index", "Adres");

                }

                else
                {
                    sendHata = "HATA";

                }

                cnn.Close();
            }
            catch (Exception ex)
            {
                test = "SQL CONNECTION NOT SUCCESSFULL";
                ViewBag.test = test;
            }




            ViewBag.sendHata = sendHata;
            return View();
        }

        public ActionResult ForgotPass ()
        {

            return View();
        }
    }
}