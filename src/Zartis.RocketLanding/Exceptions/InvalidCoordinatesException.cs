using System;

namespace Zartis.RocketLanding
{
    public class InvalidCoordinatesException : Exception
    {
        public InvalidCoordinatesException() { }
        public InvalidCoordinatesException(string message) : base(message) { }
    }
}
