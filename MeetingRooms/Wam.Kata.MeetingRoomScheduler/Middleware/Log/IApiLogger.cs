using System;

namespace Wam.Kata.MeetingRoomScheduler.Middleware.Log
{
    public interface IApiLogger
    {
        void Enable();
        void Disable();

        string Critical(Exception exception);
        string Info(string message);
    }
}