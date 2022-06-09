using System;
using System.Collections.Generic;


namespace func_rocket
{
    public static class LevelsTask
    {
        private static readonly Physics s_standardPhysics = new Physics();
        private static readonly Vector s_standartRocketPos = new Vector(200, 500);
        private static readonly Vector s_standartTargetPos = new Vector(600, 200);
        private static readonly Gravity s_whiteHole = (size, v) =>
        {
            var d = v - s_standartTargetPos;
            var l = d.Length;
            return d.Normalize() * (140 * l / (l * l + 1));
        };
        private static readonly Gravity s_blackHole = (size, v) =>
        {
            var g = (s_standartTargetPos - s_standartRocketPos) / 2 + s_standartRocketPos;
            var d = (g - v).Length;
            return (g - v).Normalize() * (300 * d / (d * d + 1));
        };


        public static IEnumerable<Level> CreateLevels()
        {
            yield return new Level("Zero",
                new Rocket(s_standartRocketPos, Vector.Zero, -Math.PI / 2),
                s_standartTargetPos,
                (size, v) => Vector.Zero, s_standardPhysics);

            yield return new Level("Heavy",
                new Rocket(s_standartRocketPos, Vector.Zero, -Math.PI / 2),
                s_standartTargetPos,
                (size, v) => new Vector(0, .9), s_standardPhysics);

            yield return new Level("Up",
                new Rocket(s_standartRocketPos, Vector.Zero, -Math.PI / 2),
                new Vector(700, 500),
                (size, v) => new Vector(0, -300.0 / (size.Height - v.Y + 300)), 
                s_standardPhysics);

            yield return new Level("WhiteHole",
                new Rocket(s_standartRocketPos, Vector.Zero, -Math.PI / 2),
                s_standartTargetPos,
                s_whiteHole,
                s_standardPhysics);

            yield return new Level("BlackHole",
                new Rocket(s_standartRocketPos, Vector.Zero, -Math.PI / 2),
                s_standartTargetPos,
                s_blackHole,
                s_standardPhysics);

            yield return new Level("BlackAndWhite",
                new Rocket(s_standartRocketPos, Vector.Zero, -Math.PI / 2),
                s_standartTargetPos,
                (size, v) => (s_whiteHole(size, v) + s_blackHole(size, v)) / 2,
                s_standardPhysics);
        }
    }
}
