using System.Net;
using System.Net.Http;
using FluentAssertions;
using TechTalk.SpecFlow;
using Wam.Kata.MeetingRoomScheduler.Test.Core;

namespace Wam.Kata.MeetingRoomScheduler.Test.MeetingRoomBooking
{
    [Binding]
    public class CancelUnknownMeetingValidRoom
    {
        private HttpResponseMessage _response;

        [When(@"I try to delete an unknown meeting on a valid room")]
        public void WhenITryToDeleteAnUnknownMeetingOnAValidRoom()
        {
            _response = Context.Server.HttpClient
                .DeleteAsync("/rooms/v1/room5/meetings/d82bb75b-246c-41b4-a2e5-d873f7accdfb").Result;
        }

        [Then(@"we must obtain an http NotFound result")]
        public void ThenWeMustObtainAnHttpNotFoundResult()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}