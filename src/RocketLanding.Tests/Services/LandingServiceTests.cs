using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AutoFixture;
using FluentAssertions;

using RocketLanding.Abstractions.Models;

namespace RocketLanding.Tests.Services
{
    public class LandingServiceTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void AskForLanding_should_throw_for_null_platform_and_rocket()
        {
            LandingPlatform platform = null;
            Rocket rocket = null;

            var landingService = new LandingService();
            Action act = () => landingService.AskForLanding(platform, rocket);

            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void AskForLanding_should_throw_for_null_platform()
        {
            LandingPlatform platform = null;
            var rocket = _fixture.Create<Rocket>();

            var landingService = new LandingService();
            Action act = () => landingService.AskForLanding(platform, rocket);

            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void AskForLanding_should_throw_for_null_rocket()
        {
            var platform = _fixture.Create<LandingPlatform>();
            Rocket rocket = null;

            var landingService = new LandingService();
            Action act = () => landingService.AskForLanding(platform, rocket);

            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void AskForLanding_should_throw_for_platform_larger_than_area()
        {
            var platform = new LandingPlatform(1, 1, 200, 200);
            var rocket = _fixture.Create<Rocket>();

            var landingService = new LandingService();
            Action act = () => landingService.AskForLanding(platform, rocket);

            act.Should().ThrowExactly<PlatformOutOfRangeException>();
        }

        [Fact]
        public void AskForLanding_should_return_out_of_platform_for_bad_rocket_coordinates()
        {
            var platform = new LandingPlatform(5, 5, 10, 10);
            var rocket = new Rocket("Apollo", 4, 4);

            var landingService = new LandingService();
            var result = landingService.AskForLanding(platform, rocket);

            result.Should().Contain("Out of platform");
        }

        [Fact]
        public void AskForLanding_should_return_ok_for_landing_for_right_rocket_coordinates()
        {
            var platform = new LandingPlatform(5, 5, 10, 10);
            var rocket = new Rocket("Apollo", 5, 5);

            var landingService = new LandingService();
            var result = landingService.AskForLanding(platform, rocket);

            result.Should().Contain("Ok for landing");
        }

        [Fact]
        public void AskForLanding_should_return_clash_for_already_taken_landing_position()
        {
            var platform = new LandingPlatform(5, 5, 10, 10);
            var apollo = new Rocket("Apollo", 5, 5);
            var spaceX = new Rocket("SpaceX", 6, 6);

            var landingService = new LandingService();
            var apolloMsg = landingService.AskForLanding(platform, apollo);
            var spaceXMsg = landingService.AskForLanding(platform, spaceX);

            apolloMsg.Should().Contain("Ok for landing");
            spaceXMsg.Should().Contain("Clash");
        }


        [Fact]
        public void AskForLanding_should_not_throw_if_rockets_try_to_land_at_same_time()
        {
            var platform = new LandingPlatform(5, 5, 10, 10);
            var rockets = new List<Rocket>
            {
                new Rocket("Apollo", 5, 5),
                new Rocket("SpaceX", 7, 7),
                new Rocket("Apollo 2", 2, 1),
                new Rocket("Apollo 3", 8, 8)
            };

            var landingService = new LandingService();
            var tasks = rockets.Select(rocket => Task<string>.Factory.StartNew(() => landingService.AskForLanding(platform, rocket))).ToArray();
            Task.WaitAll(tasks);
            Action act = () => tasks.Select(task => task.Result).ToList();

            act.Should().NotThrow();
        }
    }
}
