using System;
using Business.Data;

namespace DataAccess
{
    public abstract class SingletonBase<T> where T : class, new()
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(() => new T());
        public static FuminiHotelManagementContext _context = new FuminiHotelManagementContext();
        public static T Instance => _instance.Value;

        protected SingletonBase() { }
    }
}
