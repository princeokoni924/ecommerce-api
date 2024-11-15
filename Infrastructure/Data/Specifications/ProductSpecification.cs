using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contract.SpecificationServices;
using Core.Entities;
using Core.Params;

namespace Infrastructure.Data.Specifications
{
    public class ProductSpecification:BaseSpecifications<Product>
    {
        public ProductSpecification(ProductSpecParams specParams): base(b=>
        // search logic
        (string.IsNullOrWhiteSpace(specParams.Search)|| b.Name.ToLower().Contains(specParams.Search))&&
        (specParams.Brands.Count == 0 || specParams.Brands.Contains(b.Brand))&&
        (specParams.Types.Count == 0 || specParams.Types.Contains(b.ProductType)))
        {
          // AddCriteria(b=> string.IsNullOrWhiteSpace(productBrand) || b.Brand ==productBrand
          // && string.IsNullOrWhiteSpace(productType) || b.ProductType ==productType);
              
              // appyly the number of i want to take and the number of page i want to skip
              /*
              Logic: multiple page size by page index and subtract 1 and pass page size.
              meaning: the page is size 5*1 -1 = 0 that's skip 0 and take 5 and this wii give me the first page
              */
              ApplyPaging(specParams.PageSize *(specParams.PageIndex -1), specParams.PageSize);


            switch(specParams.Sort)
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

          
        }

    }
}
        