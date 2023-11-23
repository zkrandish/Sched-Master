using SchedMasterFinalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using SchedMasterFinalWeb.ViewModels;
using SchedMasterFinalWeb.Common;

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
                    Session["UserId"] = user.UserId;

                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");  // Redirect to home page or dashboard.
                    }else if (user.Role == "Teacher")
                    {
                        string defaultPassword = LoginUtitilty.GenerateDefaultPassword();
                        if (model.Password.Trim().Equals(defaultPassword))
                        {
                            return RedirectToAction("ChangePassword", "User", new { id = user.UserId });
                        }
                        return RedirectToAction("Index", "Teacher");
                    }
                    else if (user.Role == "Student")
                    {
                        string defaultPassword = LoginUtitilty.GenerateDefaultPassword();
                        if (model.Password.Trim().Equals(defaultPassword))
                        {
                            return RedirectToAction("ChangePassword", "User", new { id = user.UserId });
                        }
                        return RedirectToAction("Index", "StudentDashboard");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }
        // GET: User/ChangePassword
        public ActionResult ChangePassword(int id)
        {
           
            var user = db.Logins.Find(id);
            if (user == null )
            {
                return HttpNotFound();
            }

          

            // This is security best practice to prevent one user from trying to change another user's password.
            if (User.Identity.IsAuthenticated && User.Identity.Name == user.UserId.ToString())
            {
               
                
                return View("~/Views/User/ChangePassword.cshtml"); // This will present the ChangePassword.cshtml view to the user.
            }

         
            return View();
        }

        // POST: User/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(int id, ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var user = db.Logins.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                if (model.NewPassword.Equals(LoginUtitilty.GenerateDefaultPassword()))
                {
                    ModelState.AddModelError("", "New password cannot be the default password.");
                    return View();
                }



                user.Password = model.NewPassword;
                db.SaveChanges();

               
                return RedirectToAction("Login", "User");
            }

            // If we got this far, something failed, redisplay form.
            return View(model);
        }

    }
}