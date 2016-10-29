using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class ProductsController : Controller
    {
        //private FabricsEntities db = new FabricsEntities();
		ProductRepository repo = RepositoryHelper.GetProductRepository();
        // GET: Products
        public ActionResult Index()
        {
			//var repo = new ProductRepository();
			//repo.UnitOfWork = new EFUnitOfWork();

			//var result = db.Product.OrderByDescending(x => x.ProductId).Take(10).ToList();
			//var result = repo.All().OrderByDescending(x => x.ProductId).Take(10).ToList();
			var result = repo.GetAllOrderbyDescIDTake(10).ToList();
            return View(result);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			Product product = repo.Find(id.Value);
            //Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock,IsDeleted")] Product product)
        {
            if (ModelState.IsValid)
            {
				repo.Add(product);
				repo.UnitOfWork.Commit();
                //db.Product.Add(product);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			Product product = repo.Find(id.Value);
            //Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock,IsDeleted")] Product product)
        {
            if (ModelState.IsValid)
            {
				var db = repo.UnitOfWork.Context;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			//Product product = db.Product.Find(id);
			Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			Product product = repo.Find(id);
            //Product product = db.Product.Find(id);
			product.IsDeleted = true;
			//db.Product.Remove(product);
			//db.SaveChanges();
			repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
				repo.UnitOfWork.Context.Dispose();
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
