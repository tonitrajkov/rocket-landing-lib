using System.Collections.Generic;

using RocketLanding.Abstractions;
using RocketLanding.Abstractions.Models;

namespace RocketLanding
{
    public class LandingService : ILandingService
    {
        private readonly HashSet<string> _takenPositions = new HashSet<string>();
        private readonly object _lockObject = new object();


        /// <summary>
        /// Function that checks if the rocket can land on the platform
        /// Checks if the defined platform is in the landing area range
        /// </summary>
        /// <param name="platform">Must be defined, otherwise throws exception</param>
        /// <param name="rocket">Must be defined, otherwise throws exception</param>
        /// <returns></returns>
        public string AskForLanding(LandingPlatform platform, Rocket rocket)
        {
            Guard.ForNullValue(platform, nameof(platform));
            Guard.ForNullValue(rocket, nameof(rocket));
            Guard.ForProperPlatformSize(platform);

            if (!IsPositionInPlatformRange(platform, rocket.CoordinateX, rocket.CoordinateY))
            {
                return $"[{rocket.Name}] Out of platform";
            }

            // Thread locking to prevent concurrency
            lock (_lockObject)
            {
                if (IsLandingPositionAvailable(rocket))
                {
                    LandRocketOnThePlatform(platform, rocket);
                    return $"[{rocket.Name}] Ok for landing";
                }
                else
                {
                    return $"[{rocket.Name}] Clash";
                }
            }
        }

        /// <summary>
        /// Checks the requested landing position is in the landing platform range
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool IsPositionInPlatformRange(LandingPlatform platform, int x, int y)
        {
            if (x < platform.StartPositionX || x > platform.EndPositionX)
            {
                return false;
            }

            if (y < platform.StartPositionY || y > platform.EndPositionY)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if the rocket requested landing position is available on the platform
        /// </summary>
        /// <param name="rocket"></param>
        /// <returns>bool</returns>
        private bool IsLandingPositionAvailable(Rocket rocket)
        {
            var key = $"{rocket.CoordinateX},{rocket.CoordinateY}";
            return !_takenPositions.Contains(key);
        }

        /// <summary>
        /// Marks the position of the landed rocket as unavailable
        /// and marks the position surrounding as unavailable too
        /// (more than one rocket can land on the same platform at the same time and rockets
        /// need to have at least one unit separation between their landing positions)
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="rocket"></param>
        private void LandRocketOnThePlatform(LandingPlatform platform, Rocket rocket)
        {
            // clear the position from the last landed rocket
            _takenPositions.Clear();

            int startPosX = rocket.CoordinateX - 1;
            int startPosY = rocket.CoordinateY - 1;
            int endPosX = rocket.CoordinateX + 1;
            int endPosY = rocket.CoordinateY + 1;

            for (int i = startPosX; i <= endPosX; i++)
            {
                for (int j = startPosY; j <= endPosY; j++)
                {
                    // check if the positon is in the platform range
                    if (IsPositionInPlatformRange(platform, i, j))
                    {
                        var key = $"{i},{j}";
                        _takenPositions.Add(key);
                    }
                }
            }
        }
    }
}
