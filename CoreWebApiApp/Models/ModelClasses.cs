using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryRowId { get; set; }
        [Required(ErrorMessage = "Category Id is must")]
        public string CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is must")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Base Price is must")]
        //[NonnegativeAttribute(ErrorMessage = "base price cannot be negative")]
        public int BasePrice { get; set; }
        public ICollection<Product> Products { get; set; }

    }

    public class Product
    {
        [Key]
        public int ProductRowId { get; set; }
        [Required(ErrorMessage = "Product Id is must")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is must")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Manufacturer is must")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Price is must")]
        public int Price { get; set; }

        [ForeignKey("CategoryRowId")]
        public int CategoryRowId { get; set; }
        public Category Category { get; set; }
    }

    public class NonnegativeAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            int val = Convert.ToInt32(value);
            if (val <0)
            {
                return false;
            }

            return true;
        }
    }
}
