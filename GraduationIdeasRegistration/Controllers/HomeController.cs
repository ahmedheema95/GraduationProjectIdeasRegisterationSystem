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
            ViewBag.TeamID = sessionID;
            var ideaExists = DB.Ideas.FirstOrDefault(s => s.TeamID == sessionID);
            if (ideaExists == null)
            {
                ViewBag.DeptID = new SelectList(DB.Departments.ToList(), "DeptID", "DeptName");
                ViewBag.addIdea = true;
            }
            else
            {
                ViewBag.existingIdea = DB.Ideas.Include(s => s.Professors).FirstOrDefault();
                ViewBag.addIdea = false;
            }
                
            return View();
        }

        [HttpPost]
        public JsonResult RegisterIdea(TempIdea idea)
        {
            var Professors = new List<Professor>();
            foreach (var profID in idea.Professors)
            {
                Professors.Add(DB.Professors.FirstOrDefault(s => s.ProfID == profID));
            }
            StudentIdea I = new StudentIdea
            {
                IdeaName = idea.IdeaName,
                IdeaDescription = idea.IdeaDescription,
                TeamID = idea.TeamID,
                Professors = Professors,
                IdeaState = IdeaState.Pending
            };
            DB.Ideas.Add(I);
            DB.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult DepartmentProfessors(int deptID)
        //{
        //    var result = DB.Professors.Where(s => s.DeptID == deptID)
        //        .Select(s => new { s.ProfID, s.ProfName })
        //        .ToList();
        //    return Json(result,JsonRequestBehavior.AllowGet);
        //}


        [HttpGet]
        public JsonResult DepartmentProfessors()
        {
            var result = DB.Departments.Include(s => s.Professors).Select(s => new
            {
                DeptID = s.DeptID,
                DeptName = s.DeptName,
                Professors = s.Professors.Select(a => new { a.ProfID, a.ProfName })
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DropIdea(int ideaID)
        {
            var idea = DB.Ideas.Find(ideaID);
            DB.Ideas.Remove(idea);
            DB.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
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