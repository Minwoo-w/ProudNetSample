using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Utility
{
    public static class Random
    {
        private static System.Random random = new System.Random();

        public static float RandomRange(float min, float max)
        {
            float lerpValue = (float)random.NextDouble();
            return (((1 - lerpValue) * min) + (lerpValue * max));
        }

        public static float RandomRange(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
