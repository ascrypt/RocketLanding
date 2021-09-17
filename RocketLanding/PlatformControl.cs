using System;
using System.Collections.Generic;
using RocketLanding.Validations;

namespace RocketLanding
{
    public class PlatformControl
    {
        public readonly Dictionary<Position, int> AllocatedPositions = new();
        public readonly Dictionary<Guid, Position> CheckedPositions = new();
        public RocketsPositionControl RocketsPositionControl { get; }
        private Position Position { get; }

        public PlatformControl(
            int[] xyPlatform,
            int[] xyPlatformSize,
            int[] xyLandingArea)
        {
            var landingValidations = new LandingControlValidations();
            Position = new Position(
                xyPlatform,
                xyPlatformSize[0],
                xyPlatformSize[1]);

            PlatformControlInputValidations(
                xyPlatformSize,
                xyLandingArea,
                landingValidations);
            
            RocketsPositionControl = new RocketsPositionControl(this);
        }

        private void PlatformControlInputValidations(
            int[] xyLandingPlatform,
            int[] xyLandingArea,
            LandingControlValidations landingControlValidations)
        {
            landingControlValidations.Platform(xyLandingPlatform[0], xyLandingPlatform[1]);
            landingControlValidations.Area(xyLandingArea[0], xyLandingArea[1]);
        }

        public bool IsOutOfPlatform(Position position)
        {
            return position.Xy[0] < Position.Top ||
                   position.Xy[0] >= Position.Bottom ||
                   position.Xy[1] < Position.Left ||
                   position.Xy[1] >= Position.Right;
        }

        public bool IsRepeatedPosition(Guid rocketId, Position rocketPosition)
        {
            if (!CheckedPositions.ContainsKey(rocketId)) return false;

            return CheckedPositions[rocketId].Xy[0] == rocketPosition.Xy[0] &&
                   CheckedPositions[rocketId].Xy[1] == rocketPosition.Xy[1];
        }

        public bool IsPositionClash(Guid rocketId, Position rocketPosition)
        {
            if (IsRepeatedPosition(rocketId, rocketPosition))
                return false;

            var isInAllocatedPositions = AllocatedPositions.ContainsKey(rocketPosition);
            var prevPosition = RocketsPositionControl.DeletePosition(rocketId);

            if (prevPosition != null && !isInAllocatedPositions)
                RocketsPositionControl.AddRocketPosition(
                    rocketId,
                    prevPosition);

            return isInAllocatedPositions;
        }
    }
}