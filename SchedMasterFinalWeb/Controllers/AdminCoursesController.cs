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
    public class AdminCoursesController : Controller
    {
        private SchedMasterDatabaseEntities db = new SchedMasterDatabaseEntities();
        public ActionResult AddForm()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }

            ViewBag.Action = "AddCourse";
            ViewBag.FormTitle = "Add Course";
            ViewBag.ButtonLabel = "Add";


            return View("~/Views/AdminCourses/_CourseForm.cshtml");
        }


        public ActionResult UpdateForm(String courseCode)
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
                TempData["Message"] = null;
            }
            // var Course = findCourse(id)
            var course = db.Courses.FirstOrDefault(x =>x.CourseCode == courseCode);

            ViewBag.Action = "UpdateCourse";
            ViewBag.FormTitle = "Update Course";
            ViewBag.ButtonLabel = "Update";
            return View("~/Views/AdminCourses/_CourseForm.cshtml", course);
        }


        public ActionResult DeleteConfirmed(String courseCode)
        {
            var course = db.Users.Find(courseCode);
            db.Users.Remove(course);
            db.SaveChanges();

            TempData["DeleteFlag"] = true;
            TempData["Message"] = "Student: " + courseCode + " is deleted";

            return RedirectToAction("Course", "Admin");
        }
        public ActionResult AddCourse(Course course)
        {
            try
            {
                db.Courses.Add(course);
                db.SaveChanges();
                TempData["Message"] = "Course: " + course.CourseCode + " is added Succesfully";
                return RedirectToAction("Courses", "Admin");
            }
            catch (Exception)
            {
                TempData["Message"] = "could not add!";
                return RedirectToAction("AddForm", "AdminCourses");

            }
        }

        public ActionResult UpdateCourse(Course course)
        {
            

            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Course: " + course.CourseCode + " is updated Successfully";
                return RedirectToAction("Courses", "Admin");

            }
            else
            {
                TempData["Message"] = "Could not update!";
            }

            return RedirectToAction("UpdateForm", "AdminCourses", new { id = course.CourseCode });
        }

      
    }
}