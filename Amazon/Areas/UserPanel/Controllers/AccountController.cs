using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Amazon.Areas.UserPanel.Controllers
{
    public class AccountController : Controller
    {
        UnitOfWork db = new UnitOfWork();
        // GET: UserPanel/Account
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel changepassword)
        {
            if (ModelState.IsValid)
            {
                string hashOldPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(changepassword.OldPassword, "MD5");
                var user = db.UserRepository.Get(u=>u.UserName == User.Identity.Name).Single();
                if(user.Password == hashOldPassword)
                {
                    string hashNewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(changepassword.Password, "MD5");
                    user.Password = hashNewPassword;
                    db.Save();
                    ViewBag.success = true;
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "کلمه عبور فعلی درست نمی باشد");
                }
            }
            return View();
        }
    }
}