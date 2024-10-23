using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.Contract;

public interface IRepository<T> where T :BaseEntity
{
Task<T> GetProductByIdAsync(int id);
Task <IReadOnlyList<T>>ListOfAllProductAsync();
Task<T>GetProductWithSpecification(ISpecification<T> specification);
Task<IReadOnlyList<T>>ListSpecificProductAsync(ISpecification<T> specification);
Task<TResult> GetProductWithSpecification<TResult>(ISpecification<T, TResult> specification);
Task<IReadOnlyList<TResult>>ListOfProductAsync<TResult>(ISpecification<T,TResult> specification);
void Add(T entity);
void Update(T entity);
void Delete(T entity);
void Exist(T entity);
Task<int>CountAsync(ISpecification<T> specification);
}

public interface ISpecification<T, TResult> where T : BaseEntity
{
}