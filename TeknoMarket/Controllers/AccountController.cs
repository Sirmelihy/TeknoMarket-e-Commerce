using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TeknoMarket.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Logout()
        {





            FormsAuthentication.SignOut();
            //Session bilgilerini de burada sıfırlamayı unutma

            return new EmptyResult();
        }
    }
}