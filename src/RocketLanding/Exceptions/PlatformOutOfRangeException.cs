using System;

namespace RocketLanding
{
    public class PlatformOutOfRangeException : Exception
    {
        public PlatformOutOfRangeException() { }
        public PlatformOutOfRangeException(string message) : base(message) { }
    }
}
