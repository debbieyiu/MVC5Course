namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

	[MetadataType(typeof(ProductMetaData))]
	public partial class Product : IValidatableObject
	{
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			//validate model business logic
			if (this.Price > 1000 && this.Stock > 100)
			{
				yield return new ValidationResult("本商品價格與庫存量不合理", new string[] { "Price", "Stock" });
			}

			if (this.ProductName.Equals("Will", StringComparison.InvariantCultureIgnoreCase))
			{
				yield return new ValidationResult("本商品名稱已被商標註冊，無法使用", new string[] { "ProductName" });
			}

			yield break;
		}
	}

	public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<decimal> Stock { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
