using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Contract.SpecificationServices
{
    public class BaseSpecifications<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
    {
        
        protected BaseSpecifications(): this(null){}
     public Expression<Func<T, bool>>? Criteria =>criteria;
     //public Expression<Func<T, bool>>? Criteria {get; private set;};

     

        public Expression<Func<T, object>>? AscOrderBy {get; private set;}

        public Expression<Func<T, object>>? DescOrderBy {get; private set;}

        public bool IsDistinct {get; private set;}


        //   protected void AddCriteria(Expression<Func<T, bool>> CriteriaExpression)
        //   {
        //        Criteria = CriteriaExpression;
        //   }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpressionFunction)
        {
           AscOrderBy = orderByExpressionFunction;
        }
           
        protected void AddOrderByDescending(Expression<Func<T, object>>orderByDescendingExpressionFunction)
        {
            DescOrderBy = orderByDescendingExpressionFunction;
        }

        protected void ApplyDistinct()
        {
          IsDistinct = true;
        }
    }
}