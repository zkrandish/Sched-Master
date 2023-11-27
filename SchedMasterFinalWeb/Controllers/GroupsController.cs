using SchedMasterFinalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedMasterFinalWeb.Controllers
{
    public class GroupsController : Controller
    {
        private SchedMasterDatabaseEntities db = new SchedMasterDatabaseEntities();

        // GET: Courses
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            return View("Index", "~/Views/Shared/_StudentLayout.cshtml", db.Groups.ToList());
        }


        public ActionResult TeacherIndex()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            // Get all groups where UserId is null
            var groupsWithNoUser = db.Groups.Where(g => g.UserId == null).ToList();
            return View("TeacherIndex", "~/Views/Shared/_TeacherLayout.cshtml", groupsWithNoUser);
        }
    }
}