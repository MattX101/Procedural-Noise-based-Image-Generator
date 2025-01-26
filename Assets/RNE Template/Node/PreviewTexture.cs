using UnityEngine;

namespace RNE.Template.Node
{
    internal static class PreviewTexture
    {
        internal static Texture2D Generate(Color[] colors)
        {
            Texture2D texture = new Texture2D(ProjectData.Resolution, ProjectData.Resolution);

            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Point;

            texture.SetPixels(colors);
            texture.Apply();

            return texture;
        }
    }
}
