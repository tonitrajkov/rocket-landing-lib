using System;

namespace RocketLanding.Abstractions.Models
{
    public class Rocket
    {
        public string Name { get; set; }

        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public Rocket(string name, int x, int y)
        {
            Name = name;
            CoordinateX = x;
            CoordinateY = y;
        }
    }
}
