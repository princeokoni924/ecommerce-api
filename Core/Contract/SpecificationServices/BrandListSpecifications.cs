using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Contract.SpecificationServices
{
    public class BrandListSpecifications : BaseSpecProjection<Product, string>
    {
        public BrandListSpecifications()
        {
            AddSelect(b=>b.Brand);
            ApplyDistinct();
        }
    }
}