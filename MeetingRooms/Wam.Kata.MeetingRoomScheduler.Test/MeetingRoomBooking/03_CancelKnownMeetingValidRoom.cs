using System.Net;
using System.Net.Http;
using FluentAssertions;
using TechTalk.SpecFlow;
using Wam.Kata.MeetingRoomScheduler.Middleware.Entities;
using Wam.Kata.MeetingRoomScheduler.Middleware.Repositories;
using Wam.Kata.MeetingRoomScheduler.Test.Core;

namespace Wam.Kata.MeetingRoomScheduler.Test.MeetingRoomBooking
{
    [Binding]
    public class CancelKnownMeetingValidRoom
    {
        private HttpResponseMessage _response;

        [Given(@"We have the following meetings on our database")]
        public void GivenWeHaveTheFollowingMeetingsOnOurDatabase(Table table)
        {
            foreach (var row in table.Rows)
            {
                Database.Meetings.Add(row[0], new Meeting());
            }
        }

        [When(@"I try to delete the meeting (.*)")]
        public void WhenITryToDeleteAnExistingMeeting(string meetingCode)
        {
            _response = Context.Server.HttpClient
                .DeleteAsync($"/rooms/room5/meetings/{meetingCode}").Result;
        }

        [Then(@"we must obtain an http OK result")]
        public void ThenWeMustObtainAnHttpOKResult()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}