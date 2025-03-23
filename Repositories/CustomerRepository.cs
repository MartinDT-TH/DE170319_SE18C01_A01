using Business.Models;
using Business.Data;

namespace Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(FuminiHotelManagementContext context) : base(context) { }
    }
}