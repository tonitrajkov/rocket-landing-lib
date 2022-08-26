using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RocketLanding.Abstractions;
using RocketLanding.Abstractions.Models;

namespace RocketLanding.ConsoleTest
{
    public class LandingServiceTest
    {
        private readonly ILandingService _landingService;
        public LandingServiceTest()
        {
            _landingService = new LandingService();
        }

        public void LandRockets()
        {
            var platform = new LandingPlatform(5, 5, 10, 10);

            var rockets = new List<Rocket>
            {
                new Rocket("Apolo1", 5, 5),
                new Rocket("Apolo2", 7, 7),
                new Rocket("Apolo3", 8, 8),
                new Rocket("Apolo4", 7, 6),
                new Rocket("Apolo5", 16, 15),
                new Rocket("Apolo6", 2, 1),
                new Rocket("Apolo7", 7, 7),
                new Rocket("Apolo8", 5, 5)
            };

            foreach (var rocket in rockets)
            {
                var message = _landingService.AskForLanding(platform, rocket);
                Console.WriteLine(message);
            }
        }

        public void LandRocketsInParallel()
        {
            var platform = new LandingPlatform(5, 5, 10, 10);

            var rockets = new List<Rocket>
            {
                new Rocket("Apolo1", 5, 5),
                new Rocket("Apolo2", 7, 7),
                new Rocket("Apolo3", 8, 8),
                new Rocket("Apolo4", 7, 6),
                new Rocket("Apolo5", 16, 15),
                new Rocket("Apolo6", 2, 1),
                new Rocket("Apolo7", 7, 7),
                new Rocket("Apolo8", 5, 5)
            };

            var tasks = rockets.Select(rocket => Task<string>.Factory.StartNew(() => _landingService.AskForLanding(platform, rocket))).ToArray();
            Task.WaitAll(tasks);
            var messages = tasks.Select(task => task.Result).ToList();


            foreach (var message in messages)
            {
                Console.WriteLine(message);
            }
        }
    }
}
