using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QanAProject.CustomFilters;
using QandAServiceLayer;
using QandAViewModels;

namespace QanAProject.Controllers
{
    public class AccountController : Controller
    {
        IUsersService userService;
        public AccountController(IUsersService userService)
        {
            this.userService = userService;
        }

        public ActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        // GET: Account
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                Session["CurrentUserId"] = this.userService.InsertUser(registerViewModel);
                Session["CurrentUserName"] = registerViewModel.Name;
                Session["CurrentUserEmail"] = registerViewModel.Email;
                Session["CurrentUserPassword"] = registerViewModel.Password;
                Session["CurrentUserIsAdmin"] = false;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
            }
            return View();
        }
        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel lvm)
        {

            if (ModelState.IsValid)
            {
                var userDetails = this.userService.GetUsersByEmailAndPassword(lvm.Email, lvm.Password);
                if (userDetails != null)
                {
                    Session["CurrentUserId"] = userDetails.UserId;
                    Session["CurrentUserName"] = userDetails.Name;
                    Session["CurrentUserEmail"] = userDetails.Email;
                    Session["CurrentUserPassword"] = userDetails.Password;
                    Session["CurrentUserIsAdmin"] = userDetails.IsAdmin;
                    if (userDetails.IsAdmin)
                    {
                        return RedirectToRoute(new { Area = "admin", controller = "AdminHome", action = "Index" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("x", "Invlaid email/password");
                    return View(lvm);
                }
            }
            else
            {
                ModelState.AddModelError("x", "Invlaid data");
                return View(lvm);

            }

        }


        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        
        
        public ActionResult ChangeUserProfile()
        {
            int uid = Convert.ToInt32(Session["CurrentUserId"]);
            UserViewModel uvm = this.userService.GetUsersByUserID(uid);
            EditUserDetailsViewModel eudvm = new EditUserDetailsViewModel() { Name = uvm.Name, Email = uvm.Email, Mobile = uvm.Mobile, UserId = uvm.UserId };
            return View(eudvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeUserProfile(EditUserDetailsViewModel eudvm)
        {
            if (ModelState.IsValid)
            {
                eudvm.UserId = Convert.ToInt32(Session["CurrentUserId"]);
                this.userService.UpdateUserDetails(eudvm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View(eudvm);
            }
        }


        [UserAuthorizationFilterAttribute]
        public ActionResult ChangePassword()
        {
            int uid = Convert.ToInt32(Session["CurrentUserId"]);
            UserViewModel uvm = this.userService.GetUsersByUserID(uid);
            EditUserPasswordViewModel eupvm = new EditUserPasswordViewModel() { Email = uvm.Email, Password = "", ConfirmPassword = "", UserId = uvm.UserId };
            return View(eupvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        public ActionResult ChangePassword(EditUserPasswordViewModel eupvm)
        {
            if (ModelState.IsValid)
            {
                eupvm.UserId = Convert.ToInt32(Session["CurrentUserId"]);
                this.userService.UpdateUserPassword(eupvm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eupvm);
            }
        }
    }
}