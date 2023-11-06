using SchedMasterFinalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedMasterFinalWeb.Controllers
{
    public class AdminController : Controller
    {
        private SchedMasterDatabaseEntities db = new SchedMasterDatabaseEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View("Index", "~/Views/Shared/_AdminLayout.cshtml");
        }

        public ActionResult Students()
        {
            // This if to display the delete message when the user delete
            if (TempData["DeleteFlag"] != null && (bool)TempData["DeleteFlag"])
            {
                ViewBag.Message = TempData["Message"].ToString();
                TempData["Message"] = null;
            }

            // this if to display the update message when the user is updated
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
                TempData["Message"] = null;
            }

            var studentUsers = db.Users.Where(x => x.Role.Equals("Student",StringComparison.OrdinalIgnoreCase)).ToList();
            return View("Students", "~/Views/Shared/_AdminLayout.cshtml", studentUsers);
        }

        public ActionResult Teachers()
        {
            // This if to display the delete message when the user delete
            if (TempData["DeleteFlag"] != null && (bool)TempData["DeleteFlag"])
            {
                ViewBag.Message = TempData["Message"].ToString();
                TempData["Message"] = null;
            }

            // this if to display the update message when the user is updated
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
                TempData["Message"] = null;
            }

            var TeacherUsers = db.Users.Where(x => x.Role.Equals("Teacher", StringComparison.OrdinalIgnoreCase)).ToList();
            return View("Teachers", "~/Views/Shared/_AdminLayout.cshtml", TeacherUsers);
        }

       

       
    }
}