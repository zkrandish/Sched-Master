using SchedMasterFinalWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedMasterFinalWeb.Controllers
{
    public class TeacherDashboardController : Controller
    {
        private SchedMasterDatabaseEntities db = new SchedMasterDatabaseEntities();
        // GET: TeacherDashboard
        public ActionResult Index()
        {
            return View("Index", "~/Views/Shared/_TeacherLayout.cshtml");
        }

        public ActionResult ViewAllAvailableCourses()
        {
            return RedirectToAction("TeacherIndex", "Groups");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AssignGroup(string groupId)
        {
            if (!string.IsNullOrEmpty(groupId))
            {
                var group = db.Groups.FirstOrDefault(g => g.GroupId == groupId);
                if (group != null && group.UserId == null)
                {
                    int userId = Convert.ToInt32(Session["UserId"]);
                    if (HasTimeConflict(group.GroupId, userId))
                    {
                        return RedirectToAction("TeacherIndex", "Groups");
                    }
                    group.UserId = userId;
                    db.SaveChanges();

                    TempData["Message"] = "Group assigned successfully.";
                    return RedirectToAction("TeacherIndex", "Groups");
                }
                else
                {
                    TempData["Message"] = "Group is already assigned or does not exist.";
                }
            }
            else
            {
                TempData["Message"] = "Invalid group ID.";
            }

            return RedirectToAction("TeacherIndex", "Groups");
        }


        public ActionResult UnassignGroup(string groupId)
        {
            if (!string.IsNullOrEmpty(groupId))
            {
                int userId = Convert.ToInt32(Session["UserId"]);
                var group = db.Groups.FirstOrDefault(g => g.GroupId == groupId && g.UserId == userId);
                if (group != null)
                {
                    // Set the UserId to null to unassign the teacher from the group
                    group.UserId = null;
                    db.SaveChanges();

                    TempData["Message"] = "You have successfully unassigned from the group.";
                    return RedirectToAction("MyEnrollments");
                }
                else
                {
                    TempData["Message"] = "You are not assigned to the selected group or it does not exist.";
                    return RedirectToAction("MyEnrollments");
                }
            }
            else
            {
                TempData["Message"] = "Invalid group ID.";
                return RedirectToAction("MyEnrollments");
            }
        }


        public ActionResult MyEnrollments()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            if (Session["UserId"] != null)
            {
                int userId = (int)Session["UserId"];

                // Get the enrollments for the student
                var groups = db.Groups
                    .Where(e => e.UserId == userId)
                    .Include(e => e.Course)
                    .ToList();

                // Get the group IDs for the student's enrollments
                var groupIds = groups.Select(e => e.GroupId).Distinct().ToList();


                // Get the class sessions for those groups
                var classSessions = db.ClassSessions
                    .Where(cs => groupIds.Contains(cs.GroupId))
                    .Include(cs => cs.Group)
                    .ToList();


                // Combine the enrollments and class sessions into a view model if needed
                // For simplicity, we're using a Tuple here, but you should create a proper view model
                var viewModel = new Tuple<List<Group>, List<ClassSession>>(groups, classSessions);
                return View("MyEnrollments", "~/Views/Shared/_TeacherLayout.cshtml", viewModel);
            }
            else
            {
                return RedirectToAction("TeacherIndex", "TeacherDashboard");
            }
        }
        public bool HasTimeConflict(string newGroupId, int userId)
        {
            // Fech all class sesions for the new group.
            var newGroupSessions = db.ClassSessions
                                     .Where(cs => cs.GroupId == newGroupId)
                                     .ToList();

            // Fetch all class sessions for groups where the teacher is already enroolled.
            var teacherGroups = db.Groups
                                       .Where(e => e.UserId == userId)
                                       .Select(e => e.GroupId)
                                       .ToList();

            var existingGroupSessions = db.ClassSessions
                                          .Where(cs => teacherGroups.Contains(cs.GroupId))
                                          .ToList();

            // Check for ay time overlap between new grup sessons and exsting group sessions.
            foreach (var newSession in newGroupSessions)
            {
                foreach (var existingSession in existingGroupSessions)
                {
                    if (newSession.Day.Equals(existingSession.Day, StringComparison.OrdinalIgnoreCase))
                    {
                        // Check if the new session's start or end time falls within the existing session's time range.
                        if ((newSession.StartTime >= existingSession.StartTime && newSession.StartTime < existingSession.EndTime) ||
                            (newSession.EndTime > existingSession.StartTime && newSession.EndTime <= existingSession.EndTime))
                        {
                            TempData["Message"] = "The Group: " + newSession.GroupId + ". would have conflict with Group: " + existingSession.GroupId;

                            // Time conflict found.
                            return true;
                        }
                    }
                }
            }

            // No conflict found.
            return false;
        }
    }

    
}