using System;
using Wam.Kata.MeetingRoomScheduler.Middleware.Entities;

namespace Wam.Kata.MeetingRoomScheduler.Middleware.Repositories
{
    public interface IMeetingRepository
    {
        void Remove(string meetingCode);
        string Add(Meeting meeting);
        bool Exists(string meetingCode);
        bool IsMeetingInvolvesConflicts(Meeting meeting);
        AvailableSlot[] GetAvailableSlots(string room, DateTime date);
    }
}