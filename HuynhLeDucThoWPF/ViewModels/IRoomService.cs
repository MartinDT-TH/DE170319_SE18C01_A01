using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;

namespace HuynhLeDucThoWPF.ViewModels
{
    public interface IRoomService
    {
        Task CreateRoomAsync(RoomInformation room);
        Task UpdateRoomAsync(RoomInformation room);
        Task DeleteRoomAsync(int roomId);
    }
}
