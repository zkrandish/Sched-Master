using SchedMasterFinalWeb.Common;
using SchedMasterFinalWeb.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace SchedMasterFinalWeb.Controllers
{
    public class AdminStudentsController : Controller
    {
        private SchedMasterDatabaseEntities db = new SchedMasterDatabaseEntities();
        // GET: AdminStudent
        public ActionResult AddForm()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }

            ViewBag.Action = "AddStudent";
            ViewBag.FormTitle = "Add Student";
            ViewBag.ButtonLabel = "Add";


            return View("~/Views/AdminStudents/_StudentForm.cshtml");
        }

        public ActionResult UpdateForm(int id)
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }

            var user = db.Users.FirstOrDefault(x => x.UserId == id);
            ViewBag.Action = "UpdateStudent";
            ViewBag.FormTitle = "Update Student";
            ViewBag.ButtonLabel = "Update";
            return View("~/Views/AdminStudents/_StudentForm.cshtml", user);
        }


        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();

            TempData["DeleteFlag"] = true;
            TempData["Message"] = "Student: " + id + " is deleted";

            return RedirectToAction("Students", "Admin");
        }

        public ActionResult AddStudent(User user)
        {
            try
            {
                user.UserId = LoginUtitilty.GenerateRandomUniqueId();
                user.Role = "Student";

                db.Users.Add(user);

                Login loginUser = new Login
                {
                    UserId = user.UserId,
                    Password = LoginUtitilty.GenerateDefaultPassword()
                };
                db.Logins.Add(loginUser);
                db.SaveChanges();
                TempData["Message"] = "Student: " + user.UserId + " is added Succesfully";
                return RedirectToAction("Students", "Admin");
            }
            catch (System.Exception)
            {
                TempData["Message"] = "could not add!";
                return RedirectToAction("AddForm", "AdminStudents");
            }
                
           
        }

        public ActionResult UpdateStudent(User user)
        {
 
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Student: " + user.UserId + " is updated Successfully";
                return RedirectToAction("Students", "Admin");

            }
            else
            {
                TempData["Message"] = "Could not update!";
            }

            return RedirectToAction("UpdateForm", "AdminStudents", new { id = user.UserId });
        }
    }
 }

    