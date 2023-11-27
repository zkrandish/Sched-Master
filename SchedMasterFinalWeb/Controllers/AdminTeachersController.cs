using SchedMasterFinalWeb.Common;
using SchedMasterFinalWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedMasterFinalWeb.Controllers
{
    public class AdminTeachersController : Controller
    {
        private SchedMasterDatabaseEntities db = new SchedMasterDatabaseEntities();
        // GET: AdminStudent
        public ActionResult AddForm()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }

            ViewBag.Action = "AddTeacher";
            ViewBag.FormTitle = "Add Teacher";
            ViewBag.ButtonLabel = "Add";


            return View("~/Views/AdminTeachers/_TeacherForm.cshtml");
        }

        public ActionResult UpdateForm(int id)
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }

            var user = db.Users.FirstOrDefault(x => x.UserId == id);
            ViewBag.Action = "UpdateTeacher";
            ViewBag.FormTitle = "Update Teacher";
            ViewBag.ButtonLabel = "Update";
            return View("~/Views/AdminTeachers/_TeacherForm.cshtml", user);
        }


        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();

            TempData["DeleteFlag"] = true;
            TempData["Message"] = "Teacher: " + id + " is deleted";

            return RedirectToAction("Teachers", "Admin");
        }

        public ActionResult AddTeacher(User user)
        {
            try
            {
                user.UserId = LoginUtitilty.GenerateUniqueId();
                user.Role = "Teacher";

                db.Users.Add(user);

                Login loginUser = new Login
                {
                    UserId = user.UserId,
                    Password = LoginUtitilty.GenerateDefaultPassword()
                };
                db.Logins.Add(loginUser);
                db.SaveChanges();
                TempData["Message"] = "Teacher: " + user.UserId + " is added Succesfully";
                return RedirectToAction("Teachers", "Admin");
            }
            catch (Exception)
            {
                TempData["Message"] = "could not add " + user.UserId;
                return RedirectToAction("AddForm", "AdminTeachers");
               
            }
           
        }
           

        public ActionResult UpdateTeacher(User user)
        {

            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Teacher: " + user.UserId + " is updated Successfully";
                return RedirectToAction("Teachers", "Admin");

            }
            else
            {
                TempData["Message"] = "Could not update!";
            }

            return RedirectToAction("UpdateForm", "AdminTeachers", new { id = user.UserId });
        }
    }
}