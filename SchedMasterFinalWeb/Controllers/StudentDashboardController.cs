using SchedMasterFinalWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;



namespace SchedMasterFinalWeb.Controllers
{
    public class StudentDashboardController : Controller
    {
        private SchedMasterDatabaseEntities db = new SchedMasterDatabaseEntities();
        // GET: StudentDashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewAllAvailableCourses()
        {
            return RedirectToAction("Index", "Courses");
        }
        public ActionResult Enroll(string courseId)
        {
            if (string.IsNullOrWhiteSpace(courseId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Course ID is required");
            }

            var groupsForCourse = db.Groups.Where(g => g.CourseCode == courseId).ToList();
            ViewBag.GroupId = new SelectList(groupsForCourse, "GroupId", "GroupId");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Enroll(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                if (Session["UserId"] != null)
                {
                    int userId = (int)Session["UserId"];
                    enrollment.UserId = userId;

                    var existingEnrollment = db.Enrollments
                        .FirstOrDefault(e => e.UserId == userId && e.GroupId == enrollment.GroupId);

                    if (existingEnrollment == null)
                    {
                        db.Enrollments.Add(enrollment);
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "You have successfully enrolled in the course.";
                        return RedirectToAction("Index"); 

                    }
                    else
                    {
                        ModelState.AddModelError("", "You are already enrolled in this course group.");
                    }
                }
                else
                {
                   // Add an error message
                        ModelState.AddModelError("", "You are not logged in. Please log in to enroll in a course.");

                    // Reload the form or return to a different view as necessary
                    ViewBag.GroupId = new SelectList(db.Groups, "GroupId", "GroupId");
                    return View("Enroll");
                }
            }

            // If we reach here, something went wrong, or the user is not logged in
            ViewBag.GroupId = new SelectList(db.Groups, "GroupId", "GroupId", enrollment.GroupId);
            return View(enrollment);
        }
        public ActionResult MyEnrollments()
        {
            if (Session["UserId"] != null)
            {
                int userId = (int)Session["UserId"];

               
                var enrollments = db.Enrollments
                    .Where(e => e.UserId == userId)
                    .Include(e => e.Group)
                    .Include(e => e.Group.Course)
                    
                    .ToList();

                return View(enrollments);
            }
            else
            {
                return RedirectToAction("Index", "StudentDashboard");
            }
        }



    }
}