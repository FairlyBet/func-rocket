using System;
using System.Drawing;


namespace func_rocket
{
    public class ForcesTask
    {
        /// <summary>
        /// Создает делегат, возвращающий по ракете вектор силы тяги двигателей этой ракеты.
        /// Сила тяги направлена вдоль ракеты и равна по модулю forceValue.
        /// </summary>
        public static RocketForce GetThrustForce(double forceValue)
        {
            return r => new Vector(Math.Cos(r.Direction), Math.Sin(r.Direction)) * forceValue;
        }


        /// <summary>
        /// Преобразует делегат силы гравитации, в делегат силы, действующей на ракету
        /// </summary>
        public static RocketForce ConvertGravityToForce(Gravity gravity, Size spaceSize)
        {
            return r => gravity.Invoke(spaceSize, r.Location);
        }


        /// <summary>
        /// Суммирует все переданные силы, действующие на ракету, и возвращает суммарную силу.
        /// </summary>
        public static RocketForce Sum(params RocketForce[] forces)
        {
            return r =>
            {
                Vector sum = Vector.Zero;
                foreach (var item in forces)
                {
                    sum += item.Invoke(r);
                }
                return sum;
            };
        }
    }
}
