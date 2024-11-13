using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contract.SpecificationServices;
using Core.Entities;
using Core.IProjections;

namespace Infrastructure.Data.Specifications
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
       
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
        {
            // if spec is not null retrieve the cr 
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); // this is equivelent to x=>x.Brand==brand
            }

            //  if orderby is not empty, return orderby
            if(spec.AscOrderBy !=null)
            {
              query = query.OrderBy(spec.AscOrderBy);
            }

            if(spec.DescOrderBy !=null)
            {
                query = query.OrderByDescending(spec.DescOrderBy);
            }
            return query;
        }

        public static IQueryable<TResult>GetQueryProjection<TSpec, TResult>(IQueryable<T> query, ISpecificationProjection<T, TResult> spec)
        {
            if(spec.Criteria !=null)
            {
             query = query. Where(spec.Criteria);
            }

            if(spec.AscOrderBy !=null)
            { 
                query = query.OrderBy(spec.AscOrderBy);
            }

            if(spec.DescOrderBy !=null)
            {
             query = query.OrderByDescending(spec.DescOrderBy);
            }
        var selectQuery = query as IQueryable<TResult>;

         if(spec.SelectionExp !=null)
         {
            selectQuery = query.Select(spec.SelectionExp);
         }
                return selectQuery?? query.Cast<TResult>();
        }
    }
}