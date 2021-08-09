using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProductOperationApplication.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinStockQuantity { get; set; }
        public IList<Product> Products { get; set; }
        public bool IsDeleted { get; set; }

    }
}
