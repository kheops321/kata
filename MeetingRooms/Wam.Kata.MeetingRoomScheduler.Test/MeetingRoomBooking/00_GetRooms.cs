using System.Linq;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Wam.Kata.MeetingRoomScheduler.Middleware.Entities;
using Wam.Kata.MeetingRoomScheduler.Test.Core;

namespace Wam.Kata.MeetingRoomScheduler.Test.MeetingRoomBooking
{
    [Binding]
    public class GetRooms
    {
        private HttpResponseMessage _response;
        private Room[] _rooms;

        [When(@"I ask for the meeting room list")]
        public void WhenIAskForTheMeetingRoomList()
        {
            _response = Context.Server.HttpClient.GetAsync("/rooms/v1").Result;

            _rooms = JsonConvert.DeserializeObject<Room[]>(_response.Content.ReadAsStringAsync().Result);
        }

        [Then(@"we must obtain a http OK result")]
        public void ThenWeMustObtainAHttpOKResult()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Then(@"we must obtains (.*) rooms")]
        public void ThenWeMustObtainsRooms(int p0)
        {
            _rooms.Length.Should().Be(p0);
        }


        [Then(@"all room names must be unique")]
        public void ThenAllRoomNamesMustBeUnique()
        {
            _rooms.Select(x=>x.Name).Should().OnlyHaveUniqueItems();

        }
    }
}