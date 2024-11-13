using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.IProjections;

namespace Core.Contract.SpecificationServices
{
    public class BaseSpecProjection<T, TResult>(Expression<Func<T, bool>>? projectionCriteria) : BaseSpecifications<T>(projectionCriteria), ISpecificationProjection<T, TResult>
    {
        public Expression<Func<T, TResult>>? SelectionExp {get; private set;}

         //Add select
    protected void AddSelect(Expression<Func<T, TResult>> selecExpressionFunction)
    {

        SelectionExp= selecExpressionFunction;
    }
    }

   


}