//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;

//namespace DataAccess
//{
//    public class GenericDAO<T> where T : class
//    {
//        private readonly FuminiHotelManagementContext _context;
//        private readonly DbSet<T> _dbSet;

//        public GenericDAO(FuminiHotelManagementContext context)
//        {
//            _context = context;
//            _dbSet = _context.Set<T>();
//        }

//        public GenericDAO()
//        {
//        }

//        public async Task<T?> FindAsync(params object[] keyValues)
//        {
//            return await _dbSet.FindAsync(keyValues);
//        }

//        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
//        {
//            IQueryable<T> query = _dbSet.AsQueryable();

//            if (filter != null)
//                query = query.Where(filter);

//            query = ApplyIncludes(query, includeProperties);
//            return await query.ToListAsync();
//        }


//        public async Task<T?> GetByIdAsync(object id, string? includeProperties = null)
//        {
//            IQueryable<T> query = _dbSet.AsQueryable();
//            query = ApplyIncludes(query, includeProperties);
//            return await query.FirstOrDefaultAsync(e => EF.Property<object>(e, "Id") == id);
//        }


//        public async Task<T> AddAsync(T entity)
//        {
//            await _dbSet.AddAsync(entity);
//            await _context.SaveChangesAsync();
//            return entity;
//        }

//        public async Task<T> UpdateAsync(T entity)
//        {
//            _dbSet.Update(entity);
//            await _context.SaveChangesAsync();
//            return entity;
//        }

//        public async Task<bool> DeleteAsync(object id)
//        {
//            var entity = await _dbSet.FindAsync(id);
//            if (entity == null) return false;

//            _dbSet.Remove(entity);
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        private IQueryable<T> ApplyIncludes(IQueryable<T> query, string? includeProperties)
//        {
//            if (!string.IsNullOrWhiteSpace(includeProperties))
//            {
//                foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
//                {
//                    query = query.Include(includeProperty);
//                }
//            }
//            return query;
//        }
//    }
//}
