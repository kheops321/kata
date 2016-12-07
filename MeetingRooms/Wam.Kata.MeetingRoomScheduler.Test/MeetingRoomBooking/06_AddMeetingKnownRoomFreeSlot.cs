using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using FluentAssertions;
using TechTalk.SpecFlow;
using Wam.Kata.MeetingRoomScheduler.Middleware.Entities;
using Wam.Kata.MeetingRoomScheduler.Middleware.Repositories;
using Wam.Kata.MeetingRoomScheduler.Test.Core;

namespace Wam.Kata.MeetingRoomScheduler.Test.MeetingRoomBooking
{
    [Binding]
    public class AddMeetingKnownRoomFreeSlot
    {
        private HttpResponseMessage _response;
        private string _meetingCode;
        private string _room;

        [Given(@"We have the these bookings on (.*) for today")]
        public void GivenWeHaveTheTheseBookingsOnRoomForToday(string room, Table table)
        {
            _room = room;
            foreach (var row in table.Rows)
            {
                Database.Meetings.Add(
                    Guid.NewGuid().ToString(),
                    new Meeting
                    {
                        Room = room,
                        StartsAt = int.Parse(row["StartHour"]),
                        EndsAt = int.Parse(row["EndHour"]),
                        MeetingDate = DateTime.Now.Date,
                        User = "Walid"
                    });
            }
        }

        [When(@"I try to add a meeting on the same room starting at (.*) and ending at (.*) on same day")]
        public void WhenITryToAddAMeetingOnTheSameRoomStartingAtAndEndingAt(int p0, int p1)
        {
            var fakeMeeting = new Meeting { Room = _room, StartsAt = p0, EndsAt = p1, MeetingDate = DateTime.Now.Date, User = "Walid" };

            _response = Context.Server.HttpClient.PostAsync(
                "/rooms/room2/meetings",
                new ObjectContent(typeof(Meeting), fakeMeeting, new JsonMediaTypeFormatter())).Result;

            _meetingCode = _response.Content.ReadAsStringAsync().Result.Replace("\"", "");
        }
        

        [Then(@"we must obtain an http Created result")]
        public void ThenWeMustObtainAnHttpCreatedResult()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Then(@"we should obtain the code of the created meeting which must be a valid guid")]
        public void ThenWeShouldObtainTheCodeOfTheCreatedMeetingWhichMustBeAValidGuid()
        {
            Guid code;
            Guid.TryParse(_meetingCode, out code);
            code.Should().NotBeEmpty();
        }


    }
}