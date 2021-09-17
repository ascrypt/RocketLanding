using System;

namespace RocketLanding.Validations
{
    public class LandingControlValidations
    {
        internal void Platform(
            int x,
            int y)
        {
            if (!(x > 0 && y > 0))
                throw new ArgumentException("Platform X and Y points should be greater than 0.");
        }

        internal void Area(
            int x,
            int y)
        {
            if (!(x > 0 && y > 0))
                throw new ArgumentException("Area X and Y points should be greater than 0.");
        }
    }
}