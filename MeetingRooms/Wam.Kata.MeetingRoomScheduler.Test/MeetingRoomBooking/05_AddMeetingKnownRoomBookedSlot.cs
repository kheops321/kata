using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using FluentAssertions;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Wam.Kata.MeetingRoomScheduler.Middleware.Entities;
using Wam.Kata.MeetingRoomScheduler.Middleware.Repositories;
using Wam.Kata.MeetingRoomScheduler.Test.Core;

namespace Wam.Kata.MeetingRoomScheduler.Test.MeetingRoomBooking
{
    [Binding]
    public class AddMeetingKnownRoomBookedSlot
    {
        private HttpResponseMessage _response;
        private string _room;
        private AvailableSlot[] _availableSlots;

        [Given(@"We have the following bookings on (.*) for today")]
        public void GivenWeHaveTheFollowingBookingsOnRoom(string room, Table table)
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

        [When(@"I try to add a meeting on same room  and starting at (.*) and ending at (.*)")]
        public void WhenITryToAddAMeetingOnSameRoomAndStartingAtAndEndingAt(int p0, int p1)
        {
            var fakeMeeting = new Meeting { Room = _room, StartsAt = p0, EndsAt = p1, MeetingDate = DateTime.Now.Date, User = "Walid" };

            _response = Context.Server.HttpClient.PostAsync(
                "/rooms/room2/meetings",
                new ObjectContent(typeof(Meeting), fakeMeeting, new JsonMediaTypeFormatter())).Result;

            _availableSlots = JsonConvert.DeserializeObject<AvailableSlot[]>(_response.Content.ReadAsStringAsync().Result);

        }


        [Then(@"we must obtain an http Conflict result")]
        public void ThenWeMustObtainAnHttpConflictResult()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Then(@"the result must contains (.*) available slots")]
        public void ThenTheResultMustContainsAllAvailableSlots(int p0)
        {
            _availableSlots.Length.Should().Be(p0);
        }

        [Then(@"the available slots must like following")]
        public void ThenTheAvailableSlotsMustLikeFollowing(Table table)
        {
            foreach (var row in table.Rows)
            {
                var startHour = int.Parse(row["StartHour"]);
                var endHour = int.Parse(row["EndHour"]);
                _availableSlots.Count(x => x.StartsAt == startHour && x.EndsAt == endHour).Should().Be(1);
            }
        }


    }
}