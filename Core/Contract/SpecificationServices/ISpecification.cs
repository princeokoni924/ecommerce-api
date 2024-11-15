using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Contract.SpecificationServices
{
    public interface ISpecification<T> 
    {
        Expression<Func<T, bool>>? Criteria { get; }

        Expression<Func<T, object>>? AscOrderBy { get; }
        
        Expression<Func<T, object>>? DescOrderBy { get; }

        bool IsDistinct {get;}
        // pagination property
        int Take {get;}
        int Skip {get;}
        bool IsPagingEnable {get;}

        // apply criteria for count
        IQueryable<T>ApplyCriteriaCount(IQueryable<T> queryCount);
    }
}