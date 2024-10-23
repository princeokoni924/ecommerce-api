using System;
using System.Linq.Expressions;

namespace Core.Contract;

public interface ISpecification<T>
{
 Expression<Func<T,bool>> Criteria{get;}
}
