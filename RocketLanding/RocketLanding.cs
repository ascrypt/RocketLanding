using RocketLanding.Model;

namespace RocketLanding
{
    public class RocketLanding
    {
        private readonly PlatformControl _platformControl;

        public RocketLanding(PlatformControl platformControl)
        {
            _platformControl = platformControl;
        }
        
         /// <summary>
         /// Get rocket id and its position and check the landing status based on them
         /// </summary>
         /// <param name="rocket">a rocket with Guid</param>
         /// <param name="x">x point for rocket landing</param>
         /// <param name="y">y point for rocket landing</param>
         /// <returns>LandingCheckStatus</returns>
        public string GetLandingStatus(Rocket rocket, int x, int y)
        {
            var rocketPosition = new Position(new[] { x, y });

            if (_platformControl.IsOutOfPlatform(rocketPosition)) return LandingCheckStatus.OutOfPlatform;
            if (_platformControl.IsPositionClash(rocket.RocketId, rocketPosition))
                return LandingCheckStatus.Clash;
            _platformControl.RocketsPositionControl.AddRocketPosition(rocket.RocketId, rocketPosition);

            return LandingCheckStatus.OkForLanding;
        }
    }
}