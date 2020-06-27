using GraduationIdeasRegistration.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GraduationIdeasRegistration.Controllers
{
    public class HomeController : Controller
    {
        private Context DB;
        public HomeController()
        {
            DB = new Context();
        }
        public ActionResult Index()
        {
            string sessionID = Session["UserID"].ToString();
            var Idea = DB.Ideas.Where(s => s.TeamID == sessionID).FirstOrDefault();
            if (Idea != null) 
            {
                IdeaState IdeaState = DB.Ideas.Where(s => s.TeamID == sessionID)
                    .Select(s => s.IdeaState)
                    .FirstOrDefault();

                switch (IdeaState)
                {
                    case IdeaState.Pending:
                        ViewBag.IdeaState = "Pending";
                        ViewBag.IdeaName = new
                        {
                            ideaName = Idea.IdeaName,
                            ideaID = Idea.IdeaID
                        };
                        break;
                    case IdeaState.Reviewed:
                        ViewBag.IdeaState = "Reviewed";
                        var IdeaProfID = DB.ProfIdeas.Where(s => s.IdeaID == Idea.IdeaID)
                            .Select(s => s.ProfID).FirstOrDefault();
                        ViewBag.IdeaNameWithProf = new 
                        {
                            ideaName = Idea.IdeaName,
                            ideaID = Idea.IdeaID,
                            profID = IdeaProfID
                        };
                    break;
                }
            }
            else
            {
                ViewBag.IdeaState = "NotSubmitted";
            }

            return View();
        }

        [HttpGet]
        public ActionResult RegisterTeam()
        {
            return View();
        }
        [HttpPost]
        public JsonResult RegisterTeam(Student[] stdList)
        {
            DB.Students.AddRange(stdList);
            DB.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RegisterIdea()
        {
            string sessionID = Session["UserID"].ToString();
            var ideaExists = DB.Ideas.FirstOrDefault(s => s.TeamID == sessionID);
            if (ideaExists != null)
            {
                ViewBag.Departments = new SelectList(DB.Departments.ToList(), "DeptID", "DeptName");
                ViewBag.addIdea = true;
            }
            else
                ViewBag.addIdea = false;
            return View();
        }

        [HttpPost]
        public JsonResult RegisterIdea(StudentIdea idea)
        {
            DB.Ideas.Add(idea);
            DB.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DepartmentProfessors(int deptID)
        {
            var result = DB.Professors.Where(s => s.DeptID == deptID)
                .Select(s => new { s.ProfID, s.ProfName })
                .ToList();
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public void DropIdea(int ideaID)
        {
            var idea = DB.Ideas.Find(ideaID);
            DB.Ideas.Remove(idea);
            DB.SaveChanges();
        }

        [HttpGet]
        public JsonResult IdeaDetails(int ideaID)
        {
            var idea = DB.Ideas.Where(s => s.IdeaID == ideaID)
                .Include(s => s.Team)
                .FirstOrDefault();
            return Json(idea, JsonRequestBehavior.AllowGet);
        }

    }
}