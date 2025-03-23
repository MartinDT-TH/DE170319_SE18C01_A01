using System.Linq.Expressions;
using Business.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class GenericDAO<T> : SingletonBase<GenericDAO<T>> where T : class
    {
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _context.Set<T>();

            // Apply filtering if provided
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Apply eager loading if includeProperties is specified
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id, string? includeProperties = null)
        {
            IQueryable<T> query = _context.Set<T>();

            // Apply eager loading if includeProperties is specified
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                var entityEntry = await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entityEntry.Entity;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine($"[ERROR] Database issue in AddAsync: {e.Message}");
                throw;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"[ERROR] Concurrency issue in UpdateAsync: {ex.Message}");
                throw;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"[ERROR] Database issue in UpdateAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entityEntry = await _context.Set<T>().FindAsync(id);
                if (entityEntry == null)
                {
                    return false;
                }
                _context.Set<T>().Remove(entityEntry);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"[ERROR] Concurrency issue in DeleteAsync: {ex.Message}");
                throw;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"[ERROR] Database issue in DeleteAsync: {ex.Message}");
                throw;
            }
        }
    }
}
