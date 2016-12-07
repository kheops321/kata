using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using FluentAssertions;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Wam.Kata.MeetingRoomScheduler.Middleware.Entities;
using Wam.Kata.MeetingRoomScheduler.Test.Core;

namespace Wam.Kata.MeetingRoomScheduler.Test.MeetingRoomBooking
{
    [Binding]
    public class AddMeetingUnknownRoom
    {
        private HttpResponseMessage _response;

        [When(@"I try to add a meeting on an unknown room")]
        public void WhenITryToAddAMeetingOnAnUnknownRoom()
        {
            var fakeMeeting = new Meeting {StartsAt = 5, EndsAt = 10, MeetingDate = DateTime.Now, User = "Walid"};

            _response = Context.Server.HttpClient.PostAsync(
                "/rooms/UNKNOWN/meetings",
                new ObjectContent(typeof(Meeting), fakeMeeting, new JsonMediaTypeFormatter())).Result;
        }

        [Then(@"we must obtain a http BadRequest result")]
        public void ThenWeMustObtainAHttpBadRequestResult()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}