using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public abstract class BaseController : Controller
    {
		protected FabricsEntities db = new FabricsEntities();

		/// <summary>
		/// handle unknow action
		/// </summary>
		/// <param name="actioName"></param>
		protected override void HandleUnknownAction(string actioName)
		{
			this.RedirectToAction("Index").ExecuteResult(this.ControllerContext);
			//base.HandleUnknownAction(actionName);
		}
	}
}