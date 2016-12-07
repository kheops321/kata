using Wam.Kata.MeetingRoomScheduler.Middleware.Entities;

namespace Wam.Kata.MeetingRoomScheduler.Middleware.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        public Room[] GetAll()
        {
            return Database.Rooms.ToArray();
        }
    }
}