using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : BaseController
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult NotFound()
		{
			return View();
		}

		public ActionResult PartialViewTest()
		{
			return PartialView();
		}

		public ActionResult FileTest()
		{
			var filePath = Server.MapPath("~/Content/ppap.jpg");

			return File(filePath, "image/jpeg");
		}

		public ActionResult FileTest2()
		{
			var filePath = Server.MapPath("~/Content/ppap.jpg");
			return File(filePath, "image/jpeg", "ppap.jpg");
		}

		public ActionResult JsonTest()
		{
			//停用延遲載入
			db.Configuration.LazyLoadingEnabled = false;
			var data = db.Product.OrderBy(p => p.ProductId).Take(10);
			return Json(data, JsonRequestBehavior.AllowGet);
		}
    }
}