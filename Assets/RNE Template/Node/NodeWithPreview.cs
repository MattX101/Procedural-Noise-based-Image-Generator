using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    public class NodeWithPreview : RuntimeNodeEditor.Node.Node
    {
        [SerializeField]
        private RawImage _image;

        internal Texture2D Texture => _image.texture as Texture2D;

        private RenderTexture _render;

        private int _kernel;

        protected void SetPreview(ComputeBuffer buffer)
        {
            if (_render == null || _render.width != ProjectData.Resolution)
            {
                _render = new RenderTexture(ProjectData.Resolution, ProjectData.Resolution, 0, RenderTextureFormat.ARGB32);
                _render.wrapMode = TextureWrapMode.Clamp;
                _render.filterMode = FilterMode.Point;
                _render.enableRandomWrite = true;
                _render.Create();

                _image.texture = _render;
            }

            RenderTexture(buffer);
        }

        private void RenderTexture(ComputeBuffer buffer)
        {
            _kernel = ProjectData.Shader.FindKernel("RenderTexture");
            ProjectData.Shader.SetBuffer(_kernel, "colors", buffer);
            ProjectData.Shader.SetTexture(_kernel, "render", _render);
            ProjectData.Shader.SetInt("resX", ProjectData.Resolution);
            ProjectData.Shader.SetInt("resY", ProjectData.Resolution);
            ProjectData.Shader.Dispatch(
                _kernel,
                Mathf.CeilToInt((float)ProjectData.Resolution / 32.0f),
                Mathf.CeilToInt((float)ProjectData.Resolution / 32.0f),
                1);
        }

        void OnDestroy()
        {
            if (_render != null)
            {
                _render.Release();
            }
        }
    }
}
