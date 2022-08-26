using System;
using Xunit;
using FluentAssertions;

using RocketLanding.Abstractions.Models;

namespace RocketLanding.Tests.Validators
{
    public class GuardTests
    {
        [Theory]
        [InlineData(-8)]
        public void ForNegativeNumbers_should_throw_on_negative_coordinate(int coordinate)
        {
            Action act = () => Guard.ForNegativeNumbers(coordinate, nameof(coordinate));
            act.Should().ThrowExactly<InvalidCoordinatesException>();
        }

        [Theory]
        [InlineData(10)]
        [InlineData(0)]
        public void ForNegativeNumbers_should_not_throw_on_positive_coordinate(int coordinate)
        {
            Action act = () => Guard.ForNegativeNumbers(coordinate, nameof(coordinate));
            act.Should().NotThrow();
        }

        [Fact]
        public void ForNullValue_should_throw_for_null()
        {
            LandingPlatform platform = null;
            Action act = () => Guard.ForNullValue(platform, nameof(platform));
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ForNullValue_should_not_throw_on_not_null()
        {
            LandingPlatform platform = new LandingPlatform(1, 1, 1, 1);
            Action act = () => Guard.ForNullValue(platform, nameof(platform));
            act.Should().NotThrow();
        }

        [Fact]
        public void ForProperPlatformSize_should_throw_for_platform_larger_than_area()
        {
            LandingPlatform platform = new LandingPlatform(10, 10, 100, 100);
            Action act = () => Guard.ForProperPlatformSize(platform);
            act.Should().ThrowExactly<PlatformOutOfRangeException>();
        }

        [Fact]
        public void ForProperPlatformSize_should_not_throw_for_platform_smaller_than_area()
        {
            LandingPlatform platform = new LandingPlatform(10, 10, 20, 20);
            Action act = () => Guard.ForProperPlatformSize(platform);
            act.Should().NotThrow();
        }
    }
}
