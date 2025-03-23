using Business.Models;
using Business.Data;

namespace Repositories
{
    public class RoomInformationRepository : GenericRepository<Customer>
    {
        public RoomInformationRepository(FuminiHotelManagementContext context) : base(context) { }
    }
}