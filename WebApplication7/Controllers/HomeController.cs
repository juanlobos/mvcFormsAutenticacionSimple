using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }



        public ActionResult About(string nombre)
        {
            var users = new Usuarios();
            var user = users.listaUsers().Where(z => z.Nombre == nombre).FirstOrDefault();

            FormsAuthentication.SetAuthCookie(user.Nombre, false);

            var authTicket = new FormsAuthenticationTicket(1, user.Nombre, DateTime.Now, DateTime.Now.AddMinutes(2), false, user.Rol);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Response.Cookies.Add(authCookie);
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize(Roles = "Persona")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}