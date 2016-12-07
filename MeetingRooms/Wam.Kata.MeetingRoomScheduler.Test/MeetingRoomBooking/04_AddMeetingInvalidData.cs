using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using FluentAssertions;
using TechTalk.SpecFlow;
using Wam.Kata.MeetingRoomScheduler.Middleware.Entities;
using Wam.Kata.MeetingRoomScheduler.Test.Core;

namespace Wam.Kata.MeetingRoomScheduler.Test.MeetingRoomBooking
{
    [Binding]
    public class AddMeetingInvalidData
    {
        private HttpResponseMessage _response;
        
        [When(@"I try to add a meeting with invalid data")]
        public void WhenITryToAddAMeetingWithInvalidData()
        {
            var fakeMeeting = new Meeting { StartsAt = 10, EndsAt = -1, MeetingDate = DateTime.Now, User = "Walid" };

            _response = Context.Server.HttpClient.PostAsync(
                "/rooms/v1/room2/meetings",
                new ObjectContent(typeof(Meeting), fakeMeeting, new JsonMediaTypeFormatter())).Result;
        }

        [Then(@"we must obtain a BadRequest result")]
        public void ThenWeMustObtainABadRequestResult()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


    }
}