using UnityEngine;

namespace RNE.Template.Node
{
    internal static class PreviewTexture
    {
        private static Texture2D _texture;

        internal static Texture2D Generate(Color[] colors)
        {
            if (_texture == null || _texture.width != ProjectData.Resolution)
            {
                _texture = new Texture2D(ProjectData.Resolution, ProjectData.Resolution);
                _texture.wrapMode = TextureWrapMode.Clamp;
                _texture.filterMode = FilterMode.Point;
            }

            _texture.SetPixels(colors);
            _texture.Apply();

            return _texture;
        }
    }
}
