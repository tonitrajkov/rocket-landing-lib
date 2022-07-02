using Zartis.RocketLanding.Abstractions.Models;

namespace Zartis.RocketLanding.Abstractions
{
    public interface ILandingService
    {
        string AskForLanding(LandingPlatform platform, Rocket rocket);
    }
}
