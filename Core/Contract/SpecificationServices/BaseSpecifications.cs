using System.Linq.Expressions;

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

        public int Take {get; private set;}

        public int Skip {get; private set;}

        public bool IsPagingEnable {get; private set;}

        public IQueryable<T> ApplyCriteriaCount(IQueryable<T> queryCount)
        {
            if(criteria !=null)
            {
               queryCount = queryCount.Where(criteria);
            }
            return queryCount;
        }


        //   protected void AddCriteria(Expression<Func<T, bool>> CriteriaExpression)
        //   {
        //        Criteria = CriteriaExpression;
        //   }

        // functionalities
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

        // paging functionalities
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnable = true;
        }
    }
}