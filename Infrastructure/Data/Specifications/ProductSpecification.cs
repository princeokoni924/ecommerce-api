using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contract.SpecificationServices;
using Core.Entities;

namespace Infrastructure.Data.Specifications
{
    public class ProductSpecification:BaseSpecifications<Product>
    {
        public ProductSpecification(string? brand,string? sort, string? type): base(b=>
        (string.IsNullOrWhiteSpace(brand) || b.Brand == brand)&&
        (string.IsNullOrWhiteSpace(type)|| b.ProductType == type))
        {
          // AddCriteria(b=> string.IsNullOrWhiteSpace(productBrand) || b.Brand ==productBrand
          // && string.IsNullOrWhiteSpace(productType) || b.ProductType ==productType);
              
              if(!string.IsNullOrEmpty(sort)){

            switch(sort)
          {
            case "priceAsc":
            AddOrderBy(p=>p.Price);
            break;
            case "priceDesc":
            AddOrderByDescending(p=>p.Price);
            break;
            default:
            AddOrderBy(n=>n.Name);
            break;

          }

          }else{
            AddOrderBy(n=>n.Name);
          }
        }

    }
}
        