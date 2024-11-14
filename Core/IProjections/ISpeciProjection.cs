using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Contract.SpecificationServices;

namespace Core.IProjections
{
    public interface ISpecProjection<T, TResult> :ISpecification<T>
    {
        Expression<Func<T, TResult>>? Select {get;}
    }
}