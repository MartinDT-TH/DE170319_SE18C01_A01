using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

//namespace Repositories
//{
//    public interface IRepository<T> where T : class
//    {

//        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
//        //Task<T?> GetByIdAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
//        Task<T?> GetByIdAsync(int id, string? includeProperties = null);
//        //Task<T?> GetByIdAsync(int id);
//        Task<T> AddAsync(T entity);
//        Task<T> UpdateAsync(T entity);
//        Task<bool> DeleteAsync(int id);

//    }
//}

namespace Repositories
{
    public interface IRepository<T> where T : class
    {
        //Task<T?> GetByIdAsync(int id, string? includeProperties = null);
        //Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        //Task<T> AddAsync(T entity);
        //Task<T> UpdateAsync(T entity);
        //Task<bool> DeleteAsync(int id);
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}

