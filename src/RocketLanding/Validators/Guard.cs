using System;
using RocketLanding.Abstractions.Models;

namespace RocketLanding
{
    public static class Guard
    {
        private const int LandingAreaStartPositionX = 0;
        private const int LandingAreaStartPositionY = 0;
        private const int LandingAreaEndPositionX = 100;
        private const int LandingAreaEndPositionY = 100;

        public static void ForProperPlatformSize(LandingPlatform platform)
        {
            ForNegativeNumbers(platform.StartPositionX, nameof(platform.StartPositionX));
            ForNegativeNumbers(platform.EndPositionX, nameof(platform.EndPositionX));
            ForNegativeNumbers(platform.StartPositionY, nameof(platform.StartPositionY));
            ForNegativeNumbers(platform.EndPositionY, nameof(platform.EndPositionY));

            var maxPositionX = platform.StartPositionX + platform.EndPositionX;
            var maxPositionY = platform.StartPositionY + platform.EndPositionY;

            if (maxPositionX > LandingAreaEndPositionX || maxPositionY > LandingAreaEndPositionY)
            {
                throw new PlatformOutOfRangeException($"The landing platform is out of area's range. " +
                    $"The range of the platform area is ${LandingAreaEndPositionX}x${LandingAreaEndPositionY}");
            }
        }

        public static void ForNegativeNumbers(int input, string inputName)
        {
            if (input < 0)
            {
                throw new InvalidCoordinatesException($"The value needs to be positive for ${inputName}");
            }
        }

        public static void ForNullValue<InputType>(InputType input, string inputName)
        {
            if (input == null)
            {
                throw new ArgumentNullException($"A value needs to be provided for the argument ${inputName}");
            }
        }
    }
}
