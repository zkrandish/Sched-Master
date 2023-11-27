using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedMasterFinalWeb.Controllers
{
    public class TeacherDashboardController : Controller
    {
        // GET: TeacherDashboard
        public ActionResult Index()
        {
            return View("Index", "~/Views/Shared/_TeacherLayout.cshtml");
        }

        public ActionResult ViewAllAvailableCourses()
        {
            return RedirectToAction("TeacherIndex", "Groups");
        }
    }
}