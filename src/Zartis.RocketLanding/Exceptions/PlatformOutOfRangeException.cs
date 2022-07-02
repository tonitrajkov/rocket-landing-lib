using System;

namespace Zartis.RocketLanding
{
    public class PlatformOutOfRangeException : Exception
    {
        public PlatformOutOfRangeException() { }
        public PlatformOutOfRangeException(string message) : base(message) { }
    }
}
