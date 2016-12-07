using System.Collections.Generic;
using Wam.Kata.MeetingRoomScheduler.Middleware.Entities;

namespace Wam.Kata.MeetingRoomScheduler.Middleware.Repositories
{
    public interface IRoomRepository
    {
        Room[] GetAll();
    }
}