using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QandAViewModels;
using QandAServiceLayer;
using QanAProject.CustomFilters;

namespace QanAProject.Controllers
{
    public class QuestionsController : Controller
    {
        IQuestionsService qs;
        IAnswersService asr;
        ICategoriesService cs;

        public QuestionsController(IQuestionsService qs, IAnswersService asr, ICategoriesService cs)
        {
            this.qs = qs;
            this.asr = asr;
            this.cs = cs;
        }
        public ActionResult View(int id)
        {
            this.qs.UpdateQuestionViewsCount(id, 1);
            int uid = Convert.ToInt32(Session["CurrentUserId"]);
            QuestionViewModel qvm = this.qs.GetQuestionByQuestionId(id, uid);
            return View(qvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        public ActionResult AddAnswer(NewAnswerViewModel navm)
        {
            navm.UserId = Convert.ToInt32(Session["CurrentUserId"]);
            navm.AnswerDateAndTime = DateTime.Now;
            navm.VotesCount = 0;
            if (ModelState.IsValid)
            {
                this.asr.InsertAnswer(navm);
                return RedirectToAction("View", "Questions", new { id = navm.QuestionId });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                QuestionViewModel qvm = this.qs.GetQuestionByQuestionId(navm.QuestionId, navm.UserId);
                return View("View", qvm);
            }
        }

        [HttpPost]
        public ActionResult EditAnswer(EditAnswerViewModel avm)
        {
            if (ModelState.IsValid)
            {
                avm.UserId = Convert.ToInt32(Session["CurrentUserId"]);
                this.asr.UpdateAnswer(avm);
                return RedirectToAction("View", new { id = avm.QuestionId });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return RedirectToAction("View", new { id = avm.QuestionId });
            }
        }

        public ActionResult Create()
        {
            List<CategoryViewModel> categories = this.cs.GetCategories();
            ViewBag.categories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilter]
        public ActionResult Create(NewQuestionViewModel qvm)
        {
            if (ModelState.IsValid)
            {
                qvm.AnswersCount = 0;
                qvm.ViewsCount = 0;
                qvm.VotesCount = 0;
                qvm.QuestionDateAndTime = DateTime.Now;
                qvm.UserId = Convert.ToInt32(Session["CurrentUserId"]);
                this.qs.InsertQuestion(qvm);
                return RedirectToAction("Questions", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
    }
}

