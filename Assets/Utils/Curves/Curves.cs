using UnityEngine;

namespace Utils.Curves
{
    public static class LinearEaseCurves
    {
        // Cos
        public static float EaseInOutCos(float v)
        {
            return -(Mathf.Cos(Mathf.PI * v) - 1) / 2;
        }

        public static float EaseInOutCubic(float v)
        {
            return
                v < 0.5f ?
                4 * Mathf.Pow(v, 3) :
                1 - Mathf.Pow(-2 * v + 2, 3) / 2;
        }

        public static float EaseInOutQuint(float v)
        {
            return
                v < 0.5f ?
                16 * Mathf.Pow(v, 5) :
                1 - Mathf.Pow(-2 * v + 2, 5) / 2;
        }

        public static float EaseInOutCirc(float v)
        {
            return
                v < 0.5f
                ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * v, 2))) / 2
                : (Mathf.Sqrt(1 - Mathf.Pow(-2 * v + 2, 2)) + 1) / 2;
        }

        // Sin
        public static float EaseInOutIn(float v)
        {
            return -(1 - Mathf.Sin(Mathf.PI * v) - 1);
        }
    }
}
