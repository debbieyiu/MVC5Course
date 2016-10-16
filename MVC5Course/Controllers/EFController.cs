﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
		private FabricsEntities db = new FabricsEntities();
        // GET: EF
        public ActionResult Index()
        {
			var data = db.Product.Where(p => p.ProductName.Contains("White")).OrderByDescending(p=>p.ProductId);
            return View(data);
        }

		public ActionResult Create()
		{
			var product = new Product()
			{
				ProductName = "White Cat",
				Active = true,
				Price = 199,
				Stock = 5
			};
			db.Product.Add(product);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Delete(int id)
		{
			var product = db.Product.Find(id);

			//db.OrderLine.Where(p => p.ProductId == id)
			//db.OrderLine

			//remove related orderline items
			db.OrderLine.RemoveRange(product.OrderLine);
			db.Product.Remove(product);
			db.SaveChanges();

			//錯誤示範
			//foreach (var item in product.OrderLine.ToList())
			//{
			//	db.OrderLine.Remove(item);
			//	db.SaveChanges();
			//}

			return RedirectToAction("Index");
		}

		public ActionResult Details(int id)
		{
			var product = db.Product.Find(id);
			return View(product);
		}

		public ActionResult Update(int id)
		{
			var product = db.Product.Find(id);
			product.ProductName += "!";

			try
			{
				db.SaveChanges();
			}
			catch (DbEntityValidationException ex)
			{
				foreach (var entityErrors in ex.EntityValidationErrors)
				{
					foreach (var vErrors in entityErrors.ValidationErrors)
					{
						throw new DbEntityValidationException(vErrors.PropertyName + " 發生錯誤: " + vErrors.ErrorMessage);
					}
				}
				//throw;
			}
			return RedirectToAction("Index");
		}

		public ActionResult Add20Percent()
		{
			var data = db.Product.Where(p => p.ProductName.Contains("White"));
			foreach (var item in data)
			{
				if (item.Price.HasValue)
				{ 
					item.Price *= 1.2m;
				}
			}
			db.SaveChanges();
			return RedirectToAction("Index");
		}
    }
}