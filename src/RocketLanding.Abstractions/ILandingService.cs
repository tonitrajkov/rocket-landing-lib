using RocketLanding.Abstractions.Models;

namespace RocketLanding.Abstractions
{
    public interface ILandingService
    {
        string AskForLanding(LandingPlatform platform, Rocket rocket);
    }
}
