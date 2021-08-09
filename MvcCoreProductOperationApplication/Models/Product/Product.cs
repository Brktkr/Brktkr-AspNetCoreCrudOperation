using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProductOperationApplication.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public ProductCategory Category {get; set;}
        public int StockQuantity { get; set; }
        public bool IsDeleted { get; set; }
    }
}
