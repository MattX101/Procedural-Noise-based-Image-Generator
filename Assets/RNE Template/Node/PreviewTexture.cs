using UnityEngine;

namespace RNE.Template.Node
{
    internal static class PreviewTexture
    {
        internal const int Resolution = 128;
        internal static int Length => Resolution * Resolution;

        internal static Texture2D Generate(Color[] colors)
        {
            Texture2D texture = new Texture2D(Resolution, Resolution);

            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Point;

            texture.SetPixels(colors);
            texture.Apply();

            return texture;
        }
    }
}
