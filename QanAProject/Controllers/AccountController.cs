using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QandAServiceLayer;
using QandAViewModels;

namespace QanAProject.Controllers
{
    public class AccountController : Controller
    {
        IUserService userService;
        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        // GET: Account
        public ActionResult Register()
        {
            
            return View();
        }


    }
}