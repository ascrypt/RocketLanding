using System;

namespace RocketLanding.Model
{
    public class Rocket
    {
        public Rocket()
        {
            RocketId = Guid.NewGuid();
        }

        public Guid RocketId { get; }
    }
}