using NUnit.Framework;
using RocketLanding.Model;

namespace RocketLanding.Tests
{
    [TestFixture]
    public class RocketLandingTests
    {
        [SetUp]
        public void Setup()
        {
            _platformControl = new PlatformControl(
                _platformPosition,
                _platformSize,
                _areaSize);
            _rocketLanding = new RocketLanding(_platformControl);
            _position = new Position(_platformPosition);
        }

        private PlatformControl _platformControl;
        private RocketLanding _rocketLanding;
        private Position _position;
        private readonly int[] _areaSize = { 100, 100 };
        private readonly int[] _platformSize = { 10, 10 };
        private readonly int[] _platformPosition = { 5, 5 };


        [Test, TestCase(2, 2)]
        public void RocketLandingCheck_RocketPositionIsOutOfPlatform_ReturnOutOfPlatform(int xPoint, int yPoint)
        {
            var rocket = new Rocket();
            var position = _position.Xy;
            var result = _rocketLanding.GetLandingStatus(rocket, position[0] - xPoint, position[1] - yPoint);

            Assert.IsTrue(result == LandingCheckStatus.OutOfPlatform);
        }

        [Test]
        public void RocketLandingCheck_RocketPositionInPlatform_ReturnOkForLanding()
        {
            var rocket = new Rocket();
            var position = _position.Xy;
            var result = _rocketLanding.GetLandingStatus(rocket, position[0], position[1]);

            Assert.IsTrue(result == LandingCheckStatus.OkForLanding);
        }

        [Test]
        public void RocketLandingCheck_RepeatedRocketPositionCheck_ReturnClash()
        {
            var prevRocket = new Rocket();
            var currRocket = new Rocket();
            var position = _position.Xy;
            _rocketLanding.GetLandingStatus(prevRocket, position[0], position[1]);
            var result = _rocketLanding.GetLandingStatus(currRocket, position[0], position[1]);
            
            Assert.IsTrue(result == LandingCheckStatus.Clash);
        }

        [Test]
        public void RocketLandingCheck_CheckPositionNearPreviousRocketPosition_ReturnClash()
        {
            var currRocket = new Rocket();
            var lastRocket = new Rocket();
            var currRocketPosition = _position.Xy;
            var prevRocketPosition = _position.Xy;
            _rocketLanding.GetLandingStatus(lastRocket, prevRocketPosition[0], prevRocketPosition[1]);
            var result = _rocketLanding.GetLandingStatus(currRocket, currRocketPosition[0], currRocketPosition[1]);

            Assert.IsTrue(result == LandingCheckStatus.Clash);
        }

        [Test, TestCase(5, 5,7,7,9,9)]
        public void RocketLandingCheck_ValidRocketsCheckingPositions_ReturnsOkForLanding(int xPoint1, int yPoint1,int xPoint2, int yPoint2, int xPoint3, int yPoint3)
        {
            var rocket1 = new Rocket();
            var rocket2 = new Rocket();
            var rocket3 = new Rocket();
            var rocket1Position = _position.Xy;
            var rocket2Position = _position.Xy;
            var rocket3Position = _position.Xy;
            var resultRocket1 = _rocketLanding.GetLandingStatus(rocket1, rocket1Position[0] + xPoint1, rocket1Position[1] + xPoint1);
            var resultRocket2 = _rocketLanding.GetLandingStatus(rocket2, rocket2Position[0] + xPoint2 , rocket2Position[1] + yPoint2);
            var resultRocket3 = _rocketLanding.GetLandingStatus(rocket3, rocket3Position[0] + xPoint3 , rocket3Position[1] + xPoint3);

            Assert.IsTrue(resultRocket1 == LandingCheckStatus.OkForLanding);
            Assert.IsTrue(resultRocket2 == LandingCheckStatus.OkForLanding);
            Assert.IsTrue(resultRocket3 == LandingCheckStatus.OkForLanding);
        }
    }
}