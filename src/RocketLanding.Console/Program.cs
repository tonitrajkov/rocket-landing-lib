using System;
using System.Linq;

namespace RocketLanding.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var service = new LandingServiceTest();
            service.LandRockets();
            Console.Read();

        }
    }
}
