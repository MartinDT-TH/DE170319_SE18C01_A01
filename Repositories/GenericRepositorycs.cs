using System.Collections.Generic;
using System.Linq;
//using Business.Data;
using Business.Models;

namespace Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _items;

        public GenericRepository(FuminiHotelManagementContext context)
        {
            _items = typeof(T) switch
            {
                { } when typeof(T) == typeof(Customer) => context.Customers as List<T>,
                { } when typeof(T) == typeof(RoomInformation) => context.RoomInformations as List<T>,
                { } when typeof(T) == typeof(RoomType) => context.RoomTypes as List<T>,
                { } when typeof(T) == typeof(BookingReservation) => context.BookingReservations as List<T>,
                { } when typeof(T) == typeof(BookingDetail) => context.BookingDetails as List<T>,
                _ => new List<T>()
            };
        }

        public IEnumerable<T> GetAll() => _items;

        public T? GetById(int id) => _items.FirstOrDefault();

        public void Add(T entity) => _items.Add(entity);

        public void Update(T entity)
        {
            var index = _items.FindIndex(e => e.Equals(entity));
            if (index != -1)
                _items[index] = entity;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
                _items.Remove(entity);
        }
    }
}
