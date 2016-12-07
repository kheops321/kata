using System.Net;
using System.Net.Http;
using FluentAssertions;
using TechTalk.SpecFlow;
using Wam.Kata.MeetingRoomScheduler.Test.Core;

namespace Wam.Kata.MeetingRoomScheduler.Test.MeetingRoomBooking
{
    [Binding]
    public class CancelMeetingUnknownRoom
    {
        private HttpResponseMessage _response;

        [When(@"I try to delete a meeting using an unknown room")]
        public void WhenITryToDeleteAMeetingUsingAnUnknownRoom()
        {
            _response = Context.Server.HttpClient
                .DeleteAsync("/rooms/v1/unknown/meetings/d82bb75b-246c-41b4-a2e5-d873f7accdfb").Result;

        }

        [Then(@"we must obtain an http BadRequest result")]
        public void ThenWeMustObtainAnHttpBadRequestResult()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}