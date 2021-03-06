﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			//Thread.Sleep(10000);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

		public ActionResult GetTime()
		{
			return Content(DateTime.Now.ToString());
			//return View();
		}

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginViewModel login, string ReturnUrl)
		{
			if (ModelState.IsValid)
			{
				if (login.Email == "debbie.yiu.0219@gmail.com" && login.Password == "111111")
				{
					FormsAuthentication.RedirectFromLoginPage(login.Email, false);
					return Redirect(ReturnUrl ?? "/");
				}
			}
			return View();
		}
    }
}