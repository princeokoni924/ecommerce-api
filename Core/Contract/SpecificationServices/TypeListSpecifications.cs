using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Contract.SpecificationServices
{
    public class TypeListSpecifications : BaseSpecProjection<Product, string>
    {
        public TypeListSpecifications()
        {
            AddSelect(t=>t.ProductType);
            ApplyDistinct();
        }
    }
}