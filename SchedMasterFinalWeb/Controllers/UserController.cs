using SchedMasterFinalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;


namespace SchedMasterFinalWeb.Controllers
{
    public class UserController : Controller
    {
        private SchedMasterDatabaseEntities db = new SchedMasterDatabaseEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.Login model)
        {
            if (ModelState.IsValid)
            {
                // Validate user details here. This is just a placeholder.
           
                bool userExist = db.Logins.Any(x => x.UserId == model.UserId && x.Password == model.Password);
                Models.Login userLogin = db.Logins.FirstOrDefault(x => x.UserId == model.UserId && x.Password == model.Password);
                if (userExist)
                {
                    FormsAuthentication.SetAuthCookie(userLogin.UserId.ToString(), false);
                    User user = db.Users.FirstOrDefault(x => x.UserId == model.UserId);

                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");  // Redirect to home page or dashboard.
                    }else if (user.Role == "Teacher")
                    {
                        return RedirectToAction("Index", "Teacher");
                    }
                    else if (user.Role == "Student")
                    {
                        return RedirectToAction("Index", "Student");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }
    }
}