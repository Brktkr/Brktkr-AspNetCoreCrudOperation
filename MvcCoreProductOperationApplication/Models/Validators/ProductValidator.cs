using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProductOperationApplication.Models.Validators
{
    public class ProductValidator  :AbstractValidator<Product>
    {
      
        public ProductValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("product title cannot be empty").MaximumLength(200).WithMessage("Product title cannot exceed 200 characters");

            RuleFor(x => x.CategoryId).NotNull().NotEmpty().WithMessage("Do not leave the product category empty");

            RuleFor(x => x.StockQuantity).NotNull().NotEmpty().WithMessage("Do not leave the stock quantity empty");

        }

    }
}
