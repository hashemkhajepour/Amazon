﻿using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Amazon.Controllers
{
    public class AccountController : Controller
    {
        UnitOfWork db = new UnitOfWork();
        [Route("Register")]
        public ActionResult Register()
        {

            return View();
        }
        [Route("Register")]
        [HttpPost]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (!db.UserRepository.Get(u => u.Email == register.Email.Trim().ToLower()).Any())
                {
                    Users user = new Users()
                    {
                        Email = register.Email.Trim().ToLower(),
                        UserName = register.UserName,
                        ActiveCode = Guid.NewGuid().ToString(),
                        IsActive = false,
                        RegisterDate = DateTime.Now,
                        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                        RoleID = 1
                    };
                    string body = PartialToStringClass.RenderPartialView("ManageEmails", "ActivationEmail", user);
                    SendEmail.Send(user.Email, "Amazon", body);
                    db.UserRepository.Insert(user);
                    db.Save();
                    return View("RegisterSuccess", user);
                }
                else
                {
                    ModelState.AddModelError("Email", "ایمیل وارد شده قبلا استفاده شده است");
                }
            }
            return View();
        }

        public ActionResult ActiveUser(string id)
        {
            var user = db.UserRepository.Get().SingleOrDefault(u => u.ActiveCode == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.IsActive = true;
            user.ActiveCode = Guid.NewGuid().ToString();
            db.Save();
            ViewBag.UserName = user.UserName;
            return View();
        }

        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }
        [Route("Login")]
        [HttpPost]
        public ActionResult Login(LoginViewModel login ,string ReturnUrl="/")
        {
            if (ModelState.IsValid)
            {
                string hashpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                var user = db.UserRepository.Get(u => u.Email == login.Email && u.Password == hashpassword).SingleOrDefault();
                if(user != null)
                {
                    if (user.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, login.RemmeberMi);
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نیست");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری یافت نشد");
                }
            }
            return View();
        }
       
        [Route("logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
        [Route("forgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [Route("forgotPassword")]
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (ModelState.IsValid)
            {
                var user = db.UserRepository.Get(u => u.Email == forgot.Email.Trim().ToLower()).SingleOrDefault();
                if(user != null)
                {
                    if (user.IsActive)
                    {
                        string body = PartialToStringClass.RenderPartialView("ManageEmails", "RecoveryPassword", user);
                        SendEmail.Send(forgot.Email, "بازیابی کلمه عبور آمازون", body);
                        return View("SuccessForgotPassword", user);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نیست");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری با ایمیل وارد شده یافت نشد");
                }
            }
            return View();
        }
   
        public ActionResult RecoveryPassword(string id)
        {
   
            return View();
        }
        [HttpPost]
        public ActionResult RecoveryPassword(RecoveryPasswordViewModel recovery,string id)
        {
            if (ModelState.IsValid)
            {
                var user = db.UserRepository.Get(u => u.ActiveCode == id).SingleOrDefault();
                if(user == null)
                {
                    return HttpNotFound();
                }
                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(recovery.Password, "MD5");
                user.ActiveCode = Guid.NewGuid().ToString();
                db.Save();
            }
            return Redirect("/Login?recovery=true");
        }
    }

   
}