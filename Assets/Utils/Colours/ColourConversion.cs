using UnityEngine;

namespace Utils.Colour
{
    public static class ColourConversion
    {
        public static Vector3 RGBToHSL(Color c)
        {
            float min = Mathf.Min(c.r, c.g, c.b);
            float max = Mathf.Max(c.r, c.g, c.b);
            float diff = max - min;

            float hue = 0.0f;
            if      (Mathf.Approximately(max, c.r)) hue = (c.g - c.b) / diff % 6;
            else if (Mathf.Approximately(max, c.g)) hue = (c.b - c.r) / diff + 2;
            else if (Mathf.Approximately(max, c.b)) hue = (c.r - c.g) / diff + 4;
            hue *= 60;
            hue = hue < 0 ? 300 + 60 - Mathf.Abs(hue) : hue;
            hue = Mathf.Clamp(hue, 1, 359);

            float lightness = (max + min) / 2;
            float saturation = diff == 0 ? 0 : diff / (1.0f - Mathf.Abs(2.0f * lightness - 1.0f));

            return new Vector3(hue, saturation, lightness);
        }

        public static Color HSLToRGB(float hue, float saturation, float lightness)
        {
            float C = (1 - Mathf.Abs(2 * lightness - 1)) * saturation;
            float X = C * (1.0f - Mathf.Abs((hue / 60) % 2 - 1));
            float m = lightness - C / 2;

            Color rgb = Color.black;
            if      (hue is > 0 and <= 60)    rgb = new Color(C, X, 0);
            else if (hue is > 60 and <= 120)  rgb = new Color(X, C, 0);
            else if (hue is > 120 and <= 180) rgb = new Color(0, C, X);
            else if (hue is > 180 and <= 240) rgb = new Color(0, X, C);
            else if (hue is > 240 and <= 300) rgb = new Color(X, 0, C);
            else if (hue is > 300 and <= 360) rgb = new Color(C, 0, X);

            rgb.r += m;
            rgb.g += m;
            rgb.b += m;

            return rgb;
        }
        public static Color HSLToRGB(Vector3 hsl)
        {
            return HSLToRGB(hsl.x, hsl.y, hsl.z);
        }
    }
}
