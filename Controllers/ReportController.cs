using ASPNETWebAppMVCStudentApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETWebAppMVCStudentApp.Controllers
{
    public class ReportController : Controller
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: Report/Index
        public ActionResult Index()
        {
            var model = new CourseViewModel
            {
                Courses = new SelectList(db.Courses, "CourseID", "Title")
            };
            return View(model);
        }

        // POST: Report/Generate
        [HttpPost]
        public ActionResult Generate(int SelectedCourseID, string action)
        {
            return RedirectToAction("ReportPreview", new { courseId = SelectedCourseID });
        }

        public ActionResult ReportPreview(int? courseId)
        {
            if (courseId == null)
            {
                return RedirectToAction("Index");
            }

 
            var enrollments = db.Enrollments
                .Where(e => e.Courses.CourseID == courseId.Value)
                .Include("Students")
                .ToList();

            ViewBag.CourseTitle = db.Courses
                .Where(c => c.CourseID == courseId.Value)
                .Select(c => c.Title)
                .FirstOrDefault();

            return View(enrollments);
        }
    }
}