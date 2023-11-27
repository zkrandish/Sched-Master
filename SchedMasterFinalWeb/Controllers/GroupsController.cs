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
    }
}