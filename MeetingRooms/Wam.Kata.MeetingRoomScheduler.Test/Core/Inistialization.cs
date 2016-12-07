using System.Collections.Generic;
using Microsoft.Owin.Testing;
using TechTalk.SpecFlow;
using Wam.Kata.MeetingRoomScheduler.Configuration;
using Wam.Kata.MeetingRoomScheduler.Middleware.Entities;
using Wam.Kata.MeetingRoomScheduler.Middleware.Repositories;

namespace Wam.Kata.MeetingRoomScheduler.Test.Core
{
    [Binding]
    public class Inistialization
    {
        [AfterScenario]
        public static void ScenarioCleanup()
        {
            Database.Meetings = new Dictionary<string, Meeting>();
        }

        [BeforeFeature]
        public static void FeatureInitialize()
        {
            Context.Server = TestServer.Create<Startup>();
        }

        [AfterFeature]
        public static void FeatureCleanup()
        {
            Context.Server.Dispose();
        }
    }
}