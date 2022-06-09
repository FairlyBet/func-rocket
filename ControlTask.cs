using System;


namespace func_rocket
{
    public class ControlTask
    {
        public static Turn ControlRocket(Rocket rocket, Vector target)
        {
            var direction = target - rocket.Location;
            var d = rocket.Direction * .32;
            var v = rocket.Velocity.Angle * .68;
            var sum = d + v;
            if (Math.Abs(sum - direction.Angle) < .03)
            {
                return Turn.None;
            }
            else if (sum - direction.Angle > 0)
            {
                return Turn.Left;
            }
            else
            {
                return Turn.Right;
            }
        }
    }
}
