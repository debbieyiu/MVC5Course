using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
		public override IQueryable<Product> All()
		{
			return base.All().Where(p => p.IsDeleted == false);
		}
		public Product Find(int id)
		{
			return this.All().FirstOrDefault(p => p.ProductId == id);
		}

		public IQueryable<Product> GetAllOrderbyDescIDTake(int takeNumber)
		{
			return this.All().OrderByDescending(p => p.ProductId).Take(takeNumber);
		}
	}

	public  interface IProductRepository : IRepository<Product>
	{

	}
}