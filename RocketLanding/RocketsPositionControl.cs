using System;
using System.Collections.Generic;
using System.Linq;

namespace RocketLanding
{
    public class RocketsPositionControl
    {
        private readonly PlatformControl _platformControl;

        public RocketsPositionControl(PlatformControl platformControl)
        {
            _platformControl = platformControl;
        }

        public void AddRocketPosition(
            Guid rocketId,
            Position rocketPosition)
        {
            if (_platformControl.IsRepeatedPosition(rocketId, rocketPosition)) return;
            DeletePosition(rocketId);
            if (!_platformControl.CheckedPositions.ContainsKey(rocketId))
                _platformControl.CheckedPositions.Add(rocketId, rocketPosition);
            var positions = GetPositions(rocketPosition);
            foreach (var pos in positions.Where(pos => !_platformControl.IsOutOfPlatform(pos)))
            {
                if (!_platformControl.AllocatedPositions.ContainsKey(pos))
                    _platformControl.AllocatedPositions.Add(pos, 1);
                else
                    _platformControl.AllocatedPositions[pos] += 1;
            }
        }

        private IEnumerable<Position> GetPositions(Position position)
        {
            var nearPositions = position.GetNearPositions();
            var positions = new List<Position> { position };
            positions.AddRange(nearPositions);

            return positions;
        }

        public Position DeletePosition(Guid rocketId)
        {
            if (!_platformControl.CheckedPositions.ContainsKey(rocketId))
                return null;

            var prevPosition = _platformControl.CheckedPositions[rocketId];
            var allPositions = GetPositions(prevPosition);

            _platformControl.CheckedPositions.Remove(rocketId);
            foreach (var pos in allPositions.Where(pos => _platformControl.AllocatedPositions.ContainsKey(pos)))
            {
                _platformControl.AllocatedPositions[pos] -= 1;
                if (_platformControl.AllocatedPositions[pos] <= 0) _platformControl.AllocatedPositions.Remove(pos);
            }

            return prevPosition;
        }
    }
}