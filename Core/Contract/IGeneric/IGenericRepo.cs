using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contract.SpecificationServices;
using Core.Entities;
using Core.IProjections;

namespace Core.Contract.IGeneric
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        Task<T?> GetDataByIdAsync(int id);
        // specification method
        Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec);

        // the specification that sort the product
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        //projection
        //Task<T?> GetEntityWithSpecAsync<TResult>(ISpecificationProjection<T, TResult>spec);
        Task<IReadOnlyList<T>> ListAllDataAsync();
        void AddData(T addEntities);
        void DeleteData(T deleteEntities);
        void Edit(T updateEntities);
        Task<bool> SaveAllDataAsync();
        bool Exist(int id);
    }
}