using System.Collections.Generic;
using Wam.Kata.MeetingRoomScheduler.Middleware.Entities;

namespace Wam.Kata.MeetingRoomScheduler.Middleware.Repositories
{
    public static class Database
    {
        public static Dictionary<string, Meeting> Meetings;
        public static List<Room> Rooms;

        public static void InitDatabase()
        {
            Meetings = new Dictionary<string, Meeting>();

            Rooms = new List<Room>
            {
                new Room {Name = "Room0"},
                new Room {Name = "Room1"},
                new Room {Name = "Room2"},
                new Room {Name = "Room3"},
                new Room {Name = "Room4"},
                new Room {Name = "Room5"},
                new Room {Name = "Room6"},
                new Room {Name = "Room7"},
                new Room {Name = "Room8"},
                new Room {Name = "Room9"}
            };
        }
    }
}