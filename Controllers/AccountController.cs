using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ASPNETWebAppMVCStudentApp.Controllers
{
    public class AccountController : Controller
    {
        private SchoolDBEntities db = new SchoolDBEntities();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {

            var user = db.tblUser.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
               
                Session["Username"] = user.Username;
                return RedirectToAction("Index", "Departments");
            }
            else
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không chính xác!";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}