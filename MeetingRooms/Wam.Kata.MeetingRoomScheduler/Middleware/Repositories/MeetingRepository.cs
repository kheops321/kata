using System;
using System.Collections.Generic;
using System.Linq;
using Wam.Kata.MeetingRoomScheduler.Middleware.Entities;

namespace Wam.Kata.MeetingRoomScheduler.Middleware.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        public string Add(Meeting meeting)
        {
            var meetingCode = Guid.NewGuid().ToString();
            Database.Meetings.Add(meetingCode, meeting);
            return meetingCode;
        }

        public bool Exists(string meetingCode)
        {
            return Database.Meetings.ContainsKey(meetingCode);
        }

        public void Remove(string meetingCode)
        {
            Database.Meetings.Remove(meetingCode);
        }

        public bool IsMeetingInvolvesConflicts(Meeting meeting)
        {
            for (var i = meeting.StartsAt; i < meeting.EndsAt; i++)
            {
                if (SlotAlreadyBooked(meeting.Room, meeting.MeetingDate.Date, i))
                {
                    return true;
                }
            }
            return false;
        }

        public AvailableSlot[] GetAvailableSlots(string room, DateTime date)
        {
            var firstSlotPerDay = 0;
            var lastSlotPerDay = 23;

            var results = new List<AvailableSlot>();

            for (var i = firstSlotPerDay; i < lastSlotPerDay; i++)
            {
                if (!SlotAlreadyBooked(room, date, i))
                {
                    results.Add(new AvailableSlot {StartsAt = i, EndsAt = i + 1});
                }
            }

            return results.ToArray();
        }
        
        private bool SlotAlreadyBooked(string room, DateTime date, int requestedStartHour)
        {
            return
                Database.Meetings.Any(x => 
                    x.Value.Room.Equals(room, StringComparison.InvariantCultureIgnoreCase) &&
                    x.Value.MeetingDate.Equals(date) &&
                    x.Value.StartsAt <= requestedStartHour && 
                    x.Value.EndsAt > requestedStartHour);
        }
    }
}