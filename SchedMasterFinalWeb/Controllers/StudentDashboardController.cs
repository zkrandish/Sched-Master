using SchedMasterFinalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}