using UnityEngine;

namespace RNE.Template.Node.Pointer.ProjectData
{
    internal class Colors
    {
        internal static readonly Color Null = Color.black;
        internal static readonly Color Int = Color.red;
        internal static readonly Color Float = new(1.0f, 0.25f, 0.0f);
        internal static readonly Color Vector2 = new(1.0f, 0.75f, 0.0f);
        internal static readonly Color Vector3 = new(1.0f, 0.5f, 0.0f);
        internal static readonly Color Char = new(0.25f, 0.75f, 1.0f);
        internal static readonly Color String = new(0.0f, 0.5f, 1.0f);
        internal static readonly Color Bool = new(0.375f, 0.0f, 0.75f);
        internal static readonly Color Color = Color.magenta;
        internal static readonly Color ColorGradient = Color.cyan;
        internal static readonly Color Noise = new(0.75f, 0.75f, 0.75f);
        internal static readonly Color Texture = new(1.0f, 0.5f, 1.0f);
        internal static readonly Color CellularProfile = new(1.0f, 0.375f, 0.0f);
        internal static readonly Color NoiseWarpProfile = new(0.375f, 0.125f, 0.5f);
    }
}
