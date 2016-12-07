using System.Collections.Generic;
using Microsoft.Owin.Testing;
using Moq;
using TechTalk.SpecFlow;
using Wam.Kata.MeetingRoomScheduler.Configuration;
using Wam.Kata.MeetingRoomScheduler.Middleware.Entities;
using Wam.Kata.MeetingRoomScheduler.Middleware.Log;
using Wam.Kata.MeetingRoomScheduler.Middleware.Repositories;

namespace Wam.Kata.MeetingRoomScheduler.Test.Core
{
    [Binding]
    public class FeatureBinding
    {
        [AfterScenario]
        public static void ScenarioCleanup()
        {
            Database.Meetings = new Dictionary<string, Meeting>();
        }

        [BeforeFeature]
        public static void FeatureInitialize()
        {
            ApiLogger.Current = new Mock<IApiLogger>().Object;
            Context.Server = TestServer.Create<Startup>();
        }

        [AfterFeature]
        public static void FeatureCleanup()
        {
            Context.Server.Dispose();
        }
    }
}