using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QandAServiceLayer;

namespace QanAProject.Controllers
{
    public class HomeController : Controller
    {
        IQuestionsService qs;
        public HomeController(IQuestionsService questionsService)
        {
            qs = questionsService;
        }
        public ActionResult Index()
        {
            var list = qs.GetQuestions().Take(10).ToList();
            return View(list);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}