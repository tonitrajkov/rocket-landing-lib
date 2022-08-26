
namespace RocketLanding.Abstractions.Models
{
    public class LandingPlatform
    {
        public int StartPositionX { get; set; }
        public int StartPositionY { get; set; }
        public int EndPositionX { get; set; }
        public int EndPositionY { get; set; }

        public LandingPlatform(int startPosX, int startPosY, int endPosX, int endPosY)
        {
            StartPositionX = startPosX;
            StartPositionY = startPosY;
            EndPositionX = endPosX;
            EndPositionY = endPosY;
        }
    }
}
