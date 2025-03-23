using Business.Models;
using Business.Data;

namespace Repositories
{
    public class BookingReservationRepository : GenericRepository<Customer>
    {
        public BookingReservationRepository(FuminiHotelManagementContext context) : base(context) { }
    }
}