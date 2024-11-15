using Core.Contract.SpecificationServices;
using Core.Entities;
using Core.IProjections;

namespace Core.Contract.IGeneric
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        Task<T?> GetDataByIdAsync(int id);

        // specification
        Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        //projection
        Task<TResult?> GetEntityWithSpecProjectionAsync<TResult>(ISpecProjection<T, TResult>projection);
        Task<IReadOnlyList<TResult>>ListSpecProjAsync<TResult>(ISpecProjection<T,TResult> projection);
        Task<IReadOnlyList<T>> ListAllDataAsync();
        void AddData(T addEntities);
        void DeleteData(T deleteEntities);
        void Edit(T updateEntities);
        Task<bool> SaveAllDataAsync();
        bool Exist(int id);

        // getting the count and pass Ispecification interface
        Task<int> CountAsync(ISpecification<T>specCount);
    }
}