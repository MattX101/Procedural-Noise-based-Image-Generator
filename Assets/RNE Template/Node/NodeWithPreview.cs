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

        protected void SetPreview(ComputeBuffer buffer)
        {
            _render = new RenderTexture(ProjectData.Resolution, ProjectData.Resolution, 0, RenderTextureFormat.ARGB32);
            _render.wrapMode = TextureWrapMode.Clamp;
            _render.filterMode = FilterMode.Point;
            _render.enableRandomWrite = true;
            _render.Create();

            RenderTexture(buffer);

            _image.texture = _render;
        }

        private void RenderTexture(ComputeBuffer buffer)
        {
            int kernel = ProjectData.Shader.FindKernel("RenderTexture");
            ProjectData.Shader.SetBuffer(kernel, "colors", buffer);
            ProjectData.Shader.SetTexture(kernel, "render", _render);
            ProjectData.Shader.SetInt("resX", ProjectData.Resolution);
            ProjectData.Shader.SetInt("resY", ProjectData.Resolution);
            ProjectData.Shader.Dispatch(
                kernel,
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
