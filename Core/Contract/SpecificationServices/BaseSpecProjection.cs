using System.Linq.Expressions;
using Core.IProjections;

namespace Core.Contract.SpecificationServices
{
    public class BaseSpecProjection<T, TResult>(Expression<Func<T, bool>>? projectionCriteria) : BaseSpecifications<T>(projectionCriteria), ISpecProjection<T, TResult>
    {

        protected BaseSpecProjection(): this (null!)
        {
            
        }
        public Expression<Func<T, TResult>>? Select {get; private set;}

        

        //Add select
        protected void AddSelect(Expression<Func<T, TResult>> selecExpressionFunction)
    {

        Select= selecExpressionFunction;
    }
    }

   


}