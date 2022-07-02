using System;

namespace Zartis.RocketLanding.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var testService = new LandingServiceTest();

            testService.LandRockets();
            testService.LandRocketsInParallel();
            Console.Read();
        }
    }
}
