using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Wam.Kata.MeetingRoomScheduler.Filters;
using Wam.Kata.MeetingRoomScheduler.Middleware.Entities;
using Wam.Kata.MeetingRoomScheduler.Middleware.Repositories;

namespace Wam.Kata.MeetingRoomScheduler.Controllers
{
    /// <summary>
    /// Manage rooms
    /// </summary>
    [RoutePrefix("rooms")]
    public class MeetingRoomBookingController : ApiController
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IRoomRepository _roomRepository;
        
        public MeetingRoomBookingController(
            IMeetingRepository meetingRepository,
            IRoomRepository roomRepository)
        {
            _meetingRepository = meetingRepository;
            _roomRepository = roomRepository;
        }

        /// <summary>
        /// Get the list of all available meeting rooms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(Room[]))]
        public HttpResponseMessage GetRooms()
        {
            return Request.CreateResponse(_roomRepository.GetAll());
        }

        /// <summary>
        /// Book a new meeting. 
        /// </summary>
        /// <remarks>
        /// Returns the reservation code if all goes well.
        /// Returns a list of available slots if the requested one is not available.
        /// Returns bad request if room does not exists on database.
        /// </remarks>
        /// <param name="roomName">Name of the room in which book the meeting</param>
        /// <param name="meeting">the meeting object to add</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{roomName}/meetings/")]
        [ValidateModel]
        [ResponseType(typeof(AvailableSlot[]))]
        public HttpResponseMessage AddMeeting(string roomName, [FromBody]Meeting meeting)
        {
            if (_roomRepository.GetAll().All(x => !x.Name.Equals(roomName, StringComparison.InvariantCultureIgnoreCase)))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Unknown room [{roomName}], please provide a valid one.");
            }

            meeting.Room = roomName;
            meeting.MeetingDate = meeting.MeetingDate.Date;

            // Perso, je n'aime pas du tout les locks mais pour garantir une consistance transactionnelle sans trop en faire
            // pour ce KATA, le lock peut répondre au besoin (^_^)
            lock (Database.Meetings)
            {
                if (_meetingRepository.IsMeetingInvolvesConflicts(meeting))
                {
                    var availableSlots = _meetingRepository.GetAvailableSlots(meeting.Room, meeting.MeetingDate.Date);
                    return Request.CreateResponse(HttpStatusCode.Conflict, availableSlots);
                }

                var createdMeetingCode = _meetingRepository.Add(meeting);
                return Request.CreateResponse(HttpStatusCode.Created, createdMeetingCode);
            }

        }

        /// <summary>
        /// Delete an existing meeting.
        /// </summary>
        /// <remarks>
        /// Returns bad request if room does not exists on database.
        /// Returns not found if the provided meeting does not exists on database.
        /// Returns ok if all goes well.
        /// </remarks>
        /// <param name="roomName">Name of the room in which the meeting was booked</param>
        /// <param name="meetingCode">The unique identifier of a meeting which was provided on meeting booking</param>
        /// <returns>Boolean value indicates whenever the meeting has been deleted or not</returns>
        [HttpDelete]
        [Route("{roomName}/meetings/{meetingCode}")]
        [ValidateModel]
        public HttpResponseMessage DeleteMeeting(string roomName, string meetingCode)
        {
            if (_roomRepository.GetAll().All(x => !x.Name.Equals(roomName, StringComparison.InvariantCultureIgnoreCase)))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Unknown room [{roomName}], please provide a valid one.");
            }
            
            if (!_meetingRepository.Exists(meetingCode))
                return Request.CreateResponse(HttpStatusCode.NotFound, "The provided meeting does not exists, please provide a valid one");

            _meetingRepository.Remove(meetingCode);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}