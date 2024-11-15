using System;
using System.Collections.Generic;
using System.Linq;
using Core.IProjections;
using Core.Contract.IGeneric;
using Core.Contract.SpecificationServices;
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Generic
{
    public class GenericRepo<T>(StoreContext _db) : IGenericRepo<T> where T : BaseEntity
    {
        public void AddData(T addEntities)
        {
            _db.Set<T>().Add(addEntities);
        }

        public void DeleteData(T deleteEntities)
        {
            _db.Set<T>().Remove(deleteEntities);
        }

        public void Edit(T updateEntities)
        {
            _db.Set<T>().Attach(updateEntities);
            _db.Entry(updateEntities).State= EntityState.Modified;
        }

        public  bool Exist(int id)
        {
          return _db.Set<T>().Any(ex=>ex.Id == id);
        }

        public async Task<T?> GetDataByIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> specificationEntityAsync)
        {
           // return individual obj
            return await ApplySpecificationsHelper(specificationEntityAsync).FirstOrDefaultAsync();
        }

        public async Task<TResult?> GetEntityWithSpecProjectionAsync<TResult>(ISpecProjection<T, TResult> projection)
        {
            return await ApplyProjectionHelper(projection).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllDataAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> SpecAsync)
        {
            return await ApplySpecificationsHelper(SpecAsync).ToListAsync();
        }

        public async Task<IReadOnlyList<TResult>> ListSpecProjAsync<TResult>(ISpecProjection<T, TResult> projection)
        {
            return await ApplyProjectionHelper(projection).ToListAsync();
        }

        public async Task<bool> SaveAllDataAsync()
        {
            return await _db.SaveChangesAsync()>0;
        }

        private IQueryable<T> ApplySpecificationsHelper(ISpecification<T> specHelper)
        {
                // return specificationEvaluator and create a static method
                return SpecificationEvaluator<T>.GetQuery(_db.Set<T>().AsQueryable(),specHelper);
        }

        private IQueryable<TResult> ApplyProjectionHelper<TResult>(ISpecProjection<T, TResult> projection)
        {
            return SpecificationEvaluator<T>.GetQueryProjection<T,TResult>(_db.Set<T>().AsQueryable(), projection);
        }
              // counting functionality
        public async Task<int> CountAsync(ISpecification<T> specCount)
        {
            var querySpecCount = _db.Set<T>().AsQueryable();
            querySpecCount = specCount.ApplyCriteriaCount(querySpecCount);
            return await querySpecCount.CountAsync();
        }
    }
}